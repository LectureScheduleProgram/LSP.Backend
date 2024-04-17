using LSP.Core.Result;
using LSP.Entity.DTO.ValidationRule;

namespace LSP.Business.Abstract
{
    public interface IValidationService
    {
        ServiceResult<List<string>> GetValidatorTypes();
        ServiceResult<List<ValidationRuleDto>> GetValidationRules(string validatorType);
    }
}
