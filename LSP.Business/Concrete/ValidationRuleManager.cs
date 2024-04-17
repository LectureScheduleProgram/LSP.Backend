using System.Net;
using System.Reflection;
using LSP.Business.Abstract;
using LSP.Business.Constants;
using LSP.Business.ValidationRules.FluentValidation;
using LSP.Core.Result;
using LSP.Entity.DTO.ValidationRule;

namespace LSP.Business.Concrete
{
    public class ValidationRuleManager : IValidationService
    {
        private readonly Assembly _assembly;

        public ValidationRuleManager()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        public ServiceResult<List<string>> GetValidatorTypes()
        {
            var validatorTypes = GetTypes().Select(v => v.Name).ToList();
            return new ServiceResult<List<string>>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<List<string>>(validatorTypes, Messages.success, Messages.success_code)
            };
        }

        public ServiceResult<List<ValidationRuleDto>> GetValidationRules(string validatorType)
        {
            var fullPath = GetTypes().FirstOrDefault(v => v.FullName.Contains(validatorType))?.FullName;
            if (fullPath == null)
            {
                return new ServiceResult<List<ValidationRuleDto>>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<List<ValidationRuleDto>>(new List<ValidationRuleDto>(), Messages.not_found_data, Messages.not_found_data_code)
                };
            }
            var type = _assembly.GetType(fullPath);
            dynamic validatorInstance = Activator.CreateInstance(type);
            var rules = validatorInstance.GetValidationRules();

            return new ServiceResult<List<ValidationRuleDto>>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<List<ValidationRuleDto>>(rules, Messages.success, Messages.success_code)
            };
        }

        private List<Type> GetTypes()
        {
            var validatorTypes = _assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType != null &&
                            t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == typeof(BaseValidator<>))
                .ToList();
            return validatorTypes;
        }
    }
}
