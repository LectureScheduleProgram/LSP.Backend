using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Entity.DTO.Balance;
using LSP.Entity.DTO.Currency;
using LSP.Entity.DTO.User;

namespace LSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [SwaggerOperation(Summary = "Getting User", Description = "It gets user.")]
        [ProducesResponseType(typeof(SuccessDataResult<UserInformationDto>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult GetInformations()
        {
            var result = _userService.GetInformations();
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Update Users Informations ", Description = "It updates users informations.")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPut]
        public IActionResult Update(UpdateUserInformationDto dto)
        {
            var result = _userService.UpdateInformations(dto);
            return StatusCode(result.HttpStatusCode, result.Result);
        }
    }
}
