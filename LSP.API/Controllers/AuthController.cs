using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Core.Security;
using LSP.Entity.DTO;
using LSP.Entity.DTO.Authentication;

namespace LSP.Api.Controllers
{
	[SwaggerTag("Authentication Controller")]
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[SwaggerOperation(Summary = "Register to LectureSchedule ", Description = "Register API")]
		[ProducesResponseType(typeof(SuccessDataResult<SecurityResponseDto>), (int)HttpStatusCode.OK)]
		[HttpPost]
		[Route("register")]
		public IActionResult Register(RegisterDto userRegisterDto)
		{
			var result = _authService.Register(userRegisterDto);
			return StatusCode(result.HttpStatusCode, result.Result);
		}

		[SwaggerOperation(Summary = "Login to LectureSchedule ", Description = "Login API")]
		[ProducesResponseType(typeof(SuccessDataResult<SecurityResponseDto>), (int)HttpStatusCode.OK)]
		[HttpPost]
		[Route("login")]
		public IActionResult Login(LoginDto loginDto)
		{
			var result = _authService.Login(loginDto);
			return StatusCode(result.HttpStatusCode, result.Result);
		}

		// [SwaggerOperation(Summary = "Send Email Code", Description = "Send Email API, Type represents email template.")]
		// [ProducesResponseType(typeof(SuccessDataResult<SecurityCodeResponseDto>), (int)HttpStatusCode.OK)]
		// [HttpPost]
		// [Route("sendEmailCode")]
		// public IActionResult SendEmailCode(MfaCodeDto dto)
		// {
		// 	var result = _authService.SendEmailCode(dto);
		// 	return StatusCode(result.HttpStatusCode, result.Result);
		// }

		// [SwaggerOperation(Summary = "Checking Security Codes To Getting Token.", Description = "SecuritySystems.Status; 0: Passive, 1: Active")]
		// [ProducesResponseType(typeof(SuccessDataResult<AccessToken>), (int)HttpStatusCode.OK)]
		// [HttpPost]
		// [Route("checkSecuritiesCode")]
		// public IActionResult CheckSecurityCodes(SecurityWithUserControlRequestDto dto)
		// {
		// 	var result = _authService.CheckSecuritiesCode(dto);
		// 	return StatusCode(result.HttpStatusCode, result.Result);
		// }

		[Authorize]
		[SwaggerOperation(Summary = "Password Resetting", Description = "With help of security codes reset the password of user!")]
		[ProducesResponseType(typeof(SuccessDataResult<AccessToken>), (int)HttpStatusCode.OK)]
		[HttpPatch]
		[Route("passwordReset")]
		public IActionResult PasswordReset(PasswordResetDto pr)
		{
			var result = _authService.PasswordReset(pr);
			return StatusCode(result.HttpStatusCode, result.Result);
		}

		[SwaggerOperation(Summary = "Creating Password Control Code", Description = "It creates password control code to use in Forget Password Control Code Check API for identfying the user.")]
		[ProducesResponseType(typeof(SuccessDataResult<ForgetPasswordResponseDto>), (int)HttpStatusCode.OK)]
		[HttpPost]
		[Route("forgetPasswordRequest")]
		public IActionResult ForgetPassword([Required][DefaultValue("example@mail.com")] string email)
		{
			var result = _authService.ForgetPasswordRequest(email);
			return StatusCode(result.HttpStatusCode, result.Result);
		}

		[SwaggerOperation(Summary = "Checking Password Control Code", Description = "It checks the password control code which is created by Forget Password Control Code API.")]
		[ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
		[HttpPost]
		[Route("forgetPassword")]
		public IActionResult ForgetPassword(ForgetPasswordRequestDto pr)
		{
			var result = _authService.ForgetPassword(pr);
			return StatusCode(result.HttpStatusCode, result.Result);
		}

		[SwaggerOperation(Summary = "Checking Token", Description = "It checks the token is expired or not.")]
		[Authorize]
		[HttpGet]
		[Route("checkToken")]
		public IActionResult CheckToken()
		{
			return Ok();
		}
	}
}
