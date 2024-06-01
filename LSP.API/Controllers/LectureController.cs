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
    [SwaggerTag("Lecture Controller")]
    [Route("api/lecture")]
    [ApiController]
    [Authorize]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService _lectureService;

        public LectureController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        [SwaggerOperation(Summary = "Add Lecture", Description = "It Adds Lecture")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public IActionResult Add([Required] AddLectureDto request)
        {
            var result = _lectureService.Add(request);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Update Lecture", Description = "It updates Lecture")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPut]
        public IActionResult Update([Required] UpdateLectureDto request)
        {
            var result = _lectureService.Update(request);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Delete Lecture", Description = "It Deletes Lecture")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([Required][FromRoute] short id)
        {
            var result = _lectureService.Delete(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get Lecture By Id", Description = "It gets the Lecture by id")]
        [ProducesResponseType(typeof(SuccessDataResult<LectureDto>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([Required][FromRoute] short id)
        {
            var result = _lectureService.GetById(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get List of Lecture", Description = "It gets the list of Lecture")]
        [ProducesResponseType(typeof(SuccessDataResult<List<LectureDto>>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _lectureService.GetList();
            return StatusCode(result.HttpStatusCode, result.Result);
        }
    }
}