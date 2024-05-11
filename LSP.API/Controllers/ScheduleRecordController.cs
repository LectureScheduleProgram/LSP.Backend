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
    [SwaggerTag("Schedule Record Controller")]
    [Route("api/scheduleRecord")]
    [ApiController]
    [Authorize]
    public class ScheduleRecordController : ControllerBase
    {
        private readonly IScheduleRecordService _scheduleRecordService;

        public ScheduleRecordController(IScheduleRecordService scheduleRecordService)
        {
            _scheduleRecordService = scheduleRecordService;
        }

        [SwaggerOperation(Summary = "Add ScheduleRecord", Description = "It Adds ScheduleRecord")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPost]
        public IActionResult Add([Required] ScheduleRecord ScheduleRecord)
        {
            var result = _scheduleRecordService.Add(ScheduleRecord);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Update ScheduleRecord", Description = "It updates ScheduleRecord")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpPut]
        public IActionResult Update(ScheduleRecord ScheduleRecord)
        {
            var result = _scheduleRecordService.Update(ScheduleRecord);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Delete ScheduleRecord", Description = "It Deletes ScheduleRecord")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([Required][FromRoute] short id)
        {
            var result = _scheduleRecordService.Delete(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get ScheduleRecord By Id", Description = "It gets the ScheduleRecord by id")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([Required][FromRoute] short id)
        {
            var result = _scheduleRecordService.GetById(id);
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get List of ScheduleRecord", Description = "It gets the list of ScheduleRecord")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult GetList()
        {
            var result = _scheduleRecordService.GetList();
            return StatusCode(result.HttpStatusCode, result.Result);
        }
    }
}