using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using LSP.Core.Result;

namespace LSP.Core.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static IDataResult<string> GetIpAdress(this IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                if (!string.IsNullOrEmpty(httpContextAccessor?.HttpContext?.Request?.Headers["CF-CONNECTING-IP"]))
                {
                    return new SuccessDataResult<string>(httpContextAccessor?.HttpContext?.Request?.Headers["CF-CONNECTING-IP"]);
                }
                var ipAddress = httpContextAccessor?.HttpContext?.Features?.Get<IServerVariablesFeature>()["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(ipAddress))
                {
                    var addresses = ipAddress?.Split(',');
                    if (addresses?.Length != 0)
                        return new SuccessDataResult<string>(addresses?.Last());
                }

                return new SuccessDataResult<string>(httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            }
            catch (Exception e)
            {
                return new ErrorDataResult<string>(null, e?.Message, e?.InnerException?.Message);
            }
        }
    }
}
