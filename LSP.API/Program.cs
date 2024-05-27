using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using LSP.API.Extensions;
using LSP.Business.Constants;
using LSP.Business.DependencyResolvers.Autofac;
using LSP.Business.Extensions;
using LSP.Core.Extensions;
using LSP.Core.Middlewares;
using LSP.Core.Result;
using LSP.Core.Security;
using LSP.Entity.DTO.Configuration;
using LSP.API;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
	.ConfigureContainer<ContainerBuilder>(builder => { builder.RegisterModule(new AutofacBusinessModule()); });

//TODO: Bu arkada� Core i�erisindeki bir class a al�nacak!!
builder.Services.AddMvc().ConfigureApiBehaviorOptions(options =>
{
	options.InvalidModelStateResponseFactory = c =>
	{
		var errors = string.Join('\n', c.ModelState.Values.Where(v => v.Errors.Count > 0)
		  .SelectMany(v => v.Errors)
		  .Select(v => v.ErrorMessage));

		return new BadRequestObjectResult(new ErrorDataResult<bool>(false, errors, "validation_error"));
	};
});
builder.WebHost.ConfigureKestrel(serverOptions =>
{
	serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
});
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
	config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
	config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
});
var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);
builder.Services.AddSingleton(appSettings);

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "LSPSpecificOrigins",
		policy =>
		{
			policy.WithOrigins("https://lectureschedule.com",
				"http://lectureschedule.com", "http://localhost:3000");
			policy.AllowCredentials();
			policy.WithHeaders("Authorization", "LectureSchedule-Language", "Content-Type");
			policy.WithMethods("GET", "POST", "PUT", "DELETE", "PATCH");
		});
});

builder.Services.AddControllers().AddJsonOptions(opts =>
{
	opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.EnableAnnotations();
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "LSP.Api", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});

	c.OperationFilter<AuthorizeCheckOperationFilter>();

	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);
});
builder.Services.AddCloudServices(builder.Configuration);
builder.Services.AddHttpLogging(logging =>
{
	// Customize HTTP logging here.
	logging.LoggingFields = HttpLoggingFields.All;
	logging.RequestHeaders.Add("sec-ch-ua");
	logging.ResponseHeaders.Add("my-response-header");
	logging.MediaTypeOptions.AddText("application/javascript");
	logging.RequestBodyLogLimit = 4096;
	logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.SaveToken = true;
	options.RequireHttpsMetadata = false;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		//ValidIssuer = builder.Configuration.GetValue<string>("TokenOptions:Issuer"),
		//ValidAudience = builder.Configuration.GetValue<string>("TokenOptions:Audience"),
		ValidateIssuerSigningKey = true,
		IssuerSigningKey =
			new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("TokenOptions:SecurityKey")))
	};

	options.Events = new JwtBearerEvents
	{
		OnChallenge = async context =>
		{
			context.HandleResponse();

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

			var authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
			if (string.IsNullOrEmpty(authorizationHeader))
			{
				var missingToken = new ErrorDataResult<bool>(false, Messages.token_not_found, Messages.token_not_found_code);
				var jsonResult = JsonConvert.SerializeObject(missingToken, new JsonSerializerSettings
				{
					ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
				});

				await context.Response.WriteAsync(jsonResult);
			}
			else
			{
				var invalidToken = new ErrorDataResult<bool>(false, Messages.invalid_token, Messages.invalid_token_code);
				var jsonResult = JsonConvert.SerializeObject(invalidToken, new JsonSerializerSettings
				{
					ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
				});

				await context.Response.WriteAsync(jsonResult);
			}
		}
	};
});


builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHealthChecks();

var app = builder.Build();

new ApplicationInitializer().Initialize(app);

// TODO: Refactor here like E-Commerce Project
UserIdentityHelper.SetHttpContextAccessor(app.Services.GetRequiredService<IHttpContextAccessor>());

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.InjectJavascript("/js/custom-swagger-ui.js");
	});
}

app.UseHttpLogging();

app.UseCors("LSPSpecificOrigins");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.UseMiddleware<AuthenticationMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();