using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using LSP.Core.Utilities.Constants;
using System.Reflection;

namespace LSP.Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex);
                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            string message = ex.Message;

            ErrorResultValidation err = new()
            {
                data = null,
                success = false,
                message = CoreResponseMessages.error,
                messageCode = CoreResponseMessages.error_code
            };

            await httpContext.Response.WriteAsJsonAsync(err);

        }
        private async Task HandleValidationExceptionAsync(HttpContext httpContext, Exception ex)
        {
            FieldInfo[] properties = typeof(AspectMessages).GetFields();

            foreach (FieldInfo property in properties)
            {
                object propertyValue = property.GetValue(typeof(AspectMessages));
                //if (propertyValue != null && propertyValue.Equals(ex.Message))
                //    name =  property.Name;
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (short)HttpStatusCode.BadRequest;
            ErrorResultValidation err = new()
            {
                data = null,
                success = false,
                message = ex.Message,
                messageCode = CoreResponseMessages.validation_error_code
            };

            await httpContext.Response.WriteAsJsonAsync(err);

        }

        public class ErrorResultValidation
        {
            public List<object> data { get; set; }
            public bool success { get; set; }
            public string message { get; set; }
            public string messageCode { get; set; }

        }
    }
}