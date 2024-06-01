using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Entity.Concrete;
using System.ComponentModel.DataAnnotations;

namespace LSP.API.Controllers
{
    [SwaggerTag("Faculty Controller")]
    [Route("api/Faculty")]
    [ApiController]
    [Authorize]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _FacultyService;

        public FacultyController(IFacultyService FacultyService)
        {
            _FacultyService = FacultyService;
        }

        [SwaggerOperation(Summary = "Add Faculty", Description = "It Adds Faculty")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public IActionResult Add([Required][FromQuery] string name)
        {
            var result = _FacultyService.Add(name);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Update Faculty", Description = "It updates Faculty")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPut]
        public IActionResult Update(Faculty Faculty)
        {
            var result = _FacultyService.Update(Faculty);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Delete Faculty", Description = "It Deletes Faculty")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([Required][FromRoute] short id)
        {
            var result = _FacultyService.Delete(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get Faculty By Id", Description = "It gets the Faculty by id")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([Required][FromRoute] short id)
        {
            var result = _FacultyService.GetById(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get List of Faculty", Description = "It gets the list of Faculty")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _FacultyService.GetList();
            return StatusCode(result.HttpStatusCode, result.Result);
        }
    }
}