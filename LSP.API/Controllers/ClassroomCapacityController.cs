using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Entity.Concrete;
using System.ComponentModel.DataAnnotations;
using LSP.Entity.Enum.Classroom;
using Microsoft.AspNetCore.Authorization;

namespace LSP.API.Controllers
{
    [SwaggerTag("Classroom Capacity Controller")]
    [Route("api/classroomCapacity")]
    [ApiController]
    [Authorize]
    public class ClassroomCapacityController : ControllerBase
    {
        private readonly IClassroomCapacityService _classroomCapacityService;

        public ClassroomCapacityController(IClassroomCapacityService classroomCapacityService)
        {
            _classroomCapacityService = classroomCapacityService;
        }

        [SwaggerOperation(Summary = "Add ClassroomCapacity", Description = "It Adds ClassroomCapacity")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public IActionResult Add([Required][FromQuery] ClassroomCapacityEnum capacity)
        {
            var result = _classroomCapacityService.Add(capacity);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Update ClassroomCapacity", Description = "It updates ClassroomCapacity")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPut]
        public IActionResult Update(ClassroomCapacity ClassroomCapacity)
        {
            var result = _classroomCapacityService.Update(ClassroomCapacity);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Delete ClassroomCapacity", Description = "It Deletes ClassroomCapacity")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([Required][FromRoute] short id)
        {
            var result = _classroomCapacityService.Delete(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        // In here, we had a problem something like we cannot select any other classroom type or classroom capacity
        [SwaggerOperation(Summary = "Get ClassroomCapacity By Id", Description = "It gets the ClassroomCapacity by id")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([Required][FromRoute] short id)
        {
            var result = _classroomCapacityService.GetById(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }
        // There is a problem in here. It needs to get four data completely but it returns only three of them, but we have four.
        [SwaggerOperation(Summary = "Get List of ClassroomCapacity", Description = "It gets the list of ClassroomCapacity")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _classroomCapacityService.GetList();
            return StatusCode(result.HttpStatusCode, result.Result);
        }
    }
}