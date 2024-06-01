using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using LSP.Business.Abstract;
using LSP.Core.Result;
using System.ComponentModel.DataAnnotations;
using LSP.Entity.DTO.Department;

namespace LSP.API.Controllers
{
    [SwaggerTag("Department Controller")]
    [Route("api/Department")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _DepartmentService;

        public DepartmentController(IDepartmentService DepartmentService)
        {
            _DepartmentService = DepartmentService;
        }

        [SwaggerOperation(Summary = "Add Department", Description = "It Adds Department")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public IActionResult Add([Required] AddDepartmentDto request)
        {
            var result = _DepartmentService.Add(request);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Update Department", Description = "It updates Department")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPut]
        public IActionResult Update(UpdateDepartmentDto request)
        {
            var result = _DepartmentService.Update(request);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Delete Department", Description = "It Deletes Department")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([Required][FromRoute] short id)
        {
            var result = _DepartmentService.Delete(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get Department By Id", Description = "It gets the Department by id")]
        [ProducesResponseType(typeof(SuccessDataResult<DepartmentDto>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([Required][FromRoute] short id)
        {
            var result = _DepartmentService.GetById(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get List of Department", Description = "It gets the list of Department")]
        [ProducesResponseType(typeof(SuccessDataResult<List<DepartmentDto>>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _DepartmentService.GetList();
            return StatusCode(result.HttpStatusCode, result.Result);
        }
    }
}