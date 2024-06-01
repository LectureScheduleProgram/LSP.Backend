using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Entity.Concrete;
using System.ComponentModel.DataAnnotations;
using LSP.Entity.Enum.Classroom;

namespace LSP.API.Controllers
{
    [SwaggerTag("Classroom Type Controller")]
    [Route("api/classroomType")]
    [ApiController]
    [Authorize]
    public class ClassroomTypeController : ControllerBase
    {
        private readonly IClassroomTypeService _classroomTypeService;

        public ClassroomTypeController(IClassroomTypeService classroomTypeService)
        {
            _classroomTypeService = classroomTypeService;
        }

        [SwaggerOperation(Summary = "Add ClassroomType", Description = "It Adds ClassroomType")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public IActionResult Add([Required][FromQuery] ClassroomTypeEnum type)
        {
            var result = _classroomTypeService.Add(type);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Update ClassroomType", Description = "It updates ClassroomType")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPut]
        public IActionResult Update(ClassroomType ClassroomType)
        {
            var result = _classroomTypeService.Update(ClassroomType);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Delete ClassroomType", Description = "It Deletes ClassroomType")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([Required][FromRoute] short id)
        {
            var result = _classroomTypeService.Delete(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get ClassroomType By Id", Description = "It gets the ClassroomType by id")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([Required][FromRoute] short id)
        {
            var result = _classroomTypeService.GetById(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get List of ClassroomType", Description = "It gets the list of ClassroomType")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _classroomTypeService.GetList();
            return StatusCode(result.HttpStatusCode, result.Result);
        }
    }
}