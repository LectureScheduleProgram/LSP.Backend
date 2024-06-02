using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using LSP.Business.Abstract;
using LSP.Core.Result;
using System.ComponentModel.DataAnnotations;
using static LSP.Business.Concrete.DashboardManager;

namespace LSP.API.Controllers
{
    [SwaggerTag("Dashboard Controller")]
    [Route("api/dashboard")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [SwaggerOperation(Summary = "Get Statistics of Entities", Description = "It gets the statistics of entities")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("statisticsOfEntities")]
        public IActionResult GetList()
        {
            var result = _dashboardService.StatisticsOfEntities();
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get Statistics of Class Availability", Description = "It gets the statistics of class availability")]
        [ProducesResponseType(typeof(SuccessDataResult<bool>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("availabilityOfClasses")]
        public IActionResult GetAvailabilityOfClasses([Required][FromQuery] OpenCloseClassRequestDto request)
        {
            var result = _dashboardService.AvailabilityOfClasses(request);
            return StatusCode(result.HttpStatusCode, result.Result);
        }
    }
}