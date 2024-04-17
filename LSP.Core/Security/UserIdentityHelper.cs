using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LSP.Core.Security
{
    public static class UserIdentityHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static void SetUserInfo(string userId, string email)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email)
            };

            var identity = new ClaimsIdentity(claims, "custom");
            var principal = new ClaimsPrincipal(identity);

            _httpContextAccessor.HttpContext.User = principal;
        }

        public static int GetUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static string GetUserFullname()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value + " " + _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Surname)?.Value;
        }

        public static string GetUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static string GetUserPhoneNumber()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.MobilePhone)?.Value;
        }
    }
}
