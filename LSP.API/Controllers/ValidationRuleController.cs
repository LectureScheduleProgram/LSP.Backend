using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Entity.DTO.ValidationRule;

namespace LSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationRuleController : ControllerBase
    {
        private readonly IValidationService _validationService;

        public ValidationRuleController(IValidationService validationService)
        {
            _validationService = validationService;
        }

        [SwaggerOperation(Summary = "Get Validation Types", Description = "It gets type of validation list.")]
        [ProducesResponseType(typeof(SuccessDataResult<List<string>>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public IActionResult GetValidationTypes()
        {
            var result = _validationService.GetValidatorTypes();
            return StatusCode(result.HttpStatusCode, result.Result);
        }

        [SwaggerOperation(Summary = "Get Validation Rules", Description = "It gets validation rules according to given validator type.")]
        [ProducesResponseType(typeof(SuccessDataResult<List<ValidationRuleDto>>), (int)HttpStatusCode.OK)]
        [HttpGet]
        [Route("{validatorType}")]
        public IActionResult GetValidationRules(string validatorType)
        {
            var result = _validationService.GetValidationRules(validatorType);
            return StatusCode(result.HttpStatusCode, result.Result);
        }
    }
}
