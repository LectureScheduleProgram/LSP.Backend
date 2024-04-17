using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using LSP.Core.Entities.Concrete;
using LSP.Core.Extensions;
using LSP.Core.Result;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LSP.Core.Utilities.Constants;
using System.Collections.Generic;

namespace LSP.Core.Security
{
	public class JwtHelper : ITokenHelper
	{
		public IConfiguration Configuration { get; }
		private TokenOptions _tokenOptions;
		private int _accessTokenExpiration;
		private IHttpContextAccessor _context { get; }


		public JwtHelper(IConfiguration configuration, IHttpContextAccessor httpContext)
		{
			Configuration = configuration;
			_tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
			_context = httpContext;

			_accessTokenExpiration = _tokenOptions.AccessTokenExpiration;
		}

		public AccessToken CreateToken(User users, List<OperationClaims> operationClaims)
		{
			var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
			var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
			var jwt = CreateJwtSecurityToken(_tokenOptions, users, signingCredentials, operationClaims);
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var token = jwtSecurityTokenHandler.WriteToken(jwt);

			return new AccessToken
			{
				Token = token,
				Expiration = DateTime.Now.AddHours(_accessTokenExpiration)
			};
		}

		public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User users,
			SigningCredentials signingCredentials, List<OperationClaims> operationClaims)
		{
			var jwt = new JwtSecurityToken(
				//issuer: tokenOptions.Issuer,
				//audience: tokenOptions.Audience,
				expires: DateTime.Now.AddHours(_accessTokenExpiration),
				notBefore: DateTime.Now,
				claims: SetClaims(users, operationClaims),
				signingCredentials: signingCredentials
			);
			return jwt;
		}

		public AccessToken CreateJwtToken(User users)
		{
			var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
			var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
			var jwt = CreateJwtSecurityToken(_tokenOptions, users, signingCredentials, new List<OperationClaims>());
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var token = jwtSecurityTokenHandler.WriteToken(jwt);

			return new AccessToken
			{
				Token = token,
				Expiration = jwt.ValidTo
			};
		}

		public IDataResult<TokenInfo> GetTokenInfo()
		{
			try
			{
				var header = _context.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];

				if (string.IsNullOrEmpty(header))
					return new ErrorDataResult<TokenInfo>(null, CoreResponseMessages.token_not_found,
						CoreResponseMessages.token_not_found_code);

				var handler = new JwtSecurityTokenHandler();
				var token = handler.ReadToken(header) as JwtSecurityToken;

				var userId =
					ClaimEncryptionHelper.DecryptedData(token.Claims.First(e => e.Type == CustomClaimsEnum.userId.ToString())
						.Value);
				var email = ClaimEncryptionHelper.DecryptedData(token.Claims
					.First(e => e.Type == JwtRegisteredClaimNames.Email).Value);

				var tokenInfo = new TokenInfo
				{
					Email = email,
					ExpireDate = token.ValidTo
				};

				if (tokenInfo.ExpireDate < DateTime.Now)
					return new ErrorDataResult<TokenInfo>(null, CoreResponseMessages.token_expired,
						CoreResponseMessages.token_expired_code);

				UserIdentityHelper.SetUserInfo(userId, email);

				return new SuccessDataResult<TokenInfo>(tokenInfo, CoreResponseMessages.success, CoreResponseMessages.success_code);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return new ErrorDataResult<TokenInfo>(null, CoreResponseMessages.error, CoreResponseMessages.error_code);
			}
		}


		private IEnumerable<Claim> SetClaims(User users, List<OperationClaims> operationClaims)
		{
			var claims = new List<Claim>();
			claims.AddNameIdentifier(ClaimEncryptionHelper.EncryptedData(users.Id.ToString()));
			claims.AddEmail(ClaimEncryptionHelper.EncryptedData(users.Email));
			return claims;
		}

		#region Deprecated
		public IDataResult<SessionAddDto> CreateNewToken(User users)
		{
			try
			{
				Guid guid = Guid.NewGuid();
				var tokenString = HashString(guid.ToString(), "salt");
				_context.HttpContext.Request.Headers.TryGetValue(HeaderNames.UserAgent, out var userAgent);
				var ip = _context.GetIpAdress();

				//var userAgent = "local_agent";
				//var ip = "local_ip";
				var session = new SessionAddDto
				{
					UserId = users.Id,
					Ip = ip.Data ?? "IpNotFound",
					UserAgent = userAgent.ToString(),
					TokenString = tokenString
				};
				return new SuccessDataResult<SessionAddDto>(session, "Ok", "success");
			}
			catch (Exception e)
			{
				return new ErrorDataResult<SessionAddDto>(null, e.InnerException.Message, _context.ToString());
			}
		}
		#endregion



		private string HashString(string token, string salt)
		{
			using var sha = new System.Security.Cryptography.HMACSHA256();
			var tokenBytes = Encoding.UTF8.GetBytes(token + salt);
			var hasBytes = sha.ComputeHash(tokenBytes);

			var hash = BitConverter.ToString(hasBytes).Replace("-", string.Empty);
			return hash;
		}
	}
}