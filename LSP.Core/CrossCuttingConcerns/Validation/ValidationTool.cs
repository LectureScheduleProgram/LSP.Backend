using FluentValidation;
using LSP.Core.Result;
namespace LSP.Core.CrossCuttingConcerns.Validation
{
    public class ValidationTool
    {
        public static IDataResult<bool> Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                string errorMessages = "";
                foreach (var error in result.Errors)
                {
                    errorMessages += error.ErrorMessage;
                }
                ErrorResultValidation errorResult = new()
                {
                    Data = null,
                    Message = errorMessages,
                    MessageCode = result.Errors.ToString()
                };
                throw new ValidationException(errorMessages);
                //return errorResult;
                //new ErrorDataResult<bool>(false, errorMessages, result.Errors.ToString());
            }
            return new SuccessDataResult<bool>(true, null, "Ok");
        }

        public class ErrorResultValidation
        {
            public string Data { get; set; }
            public string Message { get; set; }
            public string MessageCode { get; set; }

        }
    }
}
