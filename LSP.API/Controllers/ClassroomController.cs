using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Entity.Concrete;
using System.ComponentModel.DataAnnotations;
using LSP.Entity.DTO.Lecture;
using LSP.Entity.DTO.ClassroomCapacity;

namespace LSP.API.Controllers
{
    [SwaggerTag("Classroom Controller")]
    [Route("api/classroom")]
    [ApiController]
    [Authorize]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [SwaggerOperation(Summary = "Add Classroom", Description = "It Adds Classroom")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public IActionResult Add([Required] AddClassroomDto request)
        {
            var result = _classroomService.Add(request);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Update Classroom", Description = "It updates Classroom")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPut]
        public IActionResult Update(Classroom Classroom)
        {
            var result = _classroomService.Update(Classroom);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Delete Classroom", Description = "It Deletes Classroom")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([Required][FromRoute] short id)
        {
            var result = _classroomService.Delete(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get Classroom By Id", Description = "It gets the Classroom by id")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([Required][FromRoute] short id)
        {
            var result = _classroomService.GetById(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get List of Classroom", Description = "It gets the list of Classroom")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _classroomService.GetList();
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get Available List of Classroom", Description = "It gets the available list of Classroom")]
        [ProducesResponseType(typeof(SuccessDataResult<List<GetAvailableClassroomResponseDto>>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("availableClassroomList")]
        public IActionResult GetAvailableClassroomList([FromQuery] GetAvailableClassroomListRequestDto request)
        {
            var result = _classroomService.GetAvailableClassroomList(request);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get Available of Classroom", Description = "It gets the available of Classroom")]
        [ProducesResponseType(typeof(SuccessDataResult<GetAvailableClassroomResponseDto>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("availableClassroom")]
        public IActionResult GetAvailableClassroom([FromQuery] GetAvailableClassroomRequestDto request)
        {
            var result = _classroomService.GetAvailableClassroom(request);
            return StatusCode(result.HttpStatusCode, result.Result);
        }
    }
}