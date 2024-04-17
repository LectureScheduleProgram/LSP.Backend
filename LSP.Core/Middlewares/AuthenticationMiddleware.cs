using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using System.Reflection;
using LSP.Core.Result;
using LSP.Core.Security;

namespace LSP.Core.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenHelper _tokenHelper;

    public AuthenticationMiddleware(RequestDelegate next, ITokenHelper tokenHelper)
    {
        _next = next;
        _tokenHelper = tokenHelper;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var endpoint = httpContext.GetEndpoint();
        var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
        if (actionDescriptor != null &&
            (actionDescriptor.ControllerTypeInfo.GetCustomAttributes<AuthorizeAttribute>().Any() ||
             actionDescriptor.MethodInfo.GetCustomAttributes<AuthorizeAttribute>().Any()))
        {
            var result = _tokenHelper.GetTokenInfo();
            if (result.Success)
                await _next(httpContext);
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                httpContext.Response.ContentType = "application/json";

                var errorMessageResult = new ErrorDataResult<object>(null, result.Message, result.MessageCode);
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorMessageResult));
            }
        }
        else
        {
            await _next(httpContext);
        }
    }
}