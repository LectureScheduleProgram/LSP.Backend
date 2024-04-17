using FluentValidation;
using LSP.Entity.DTO;

namespace LSP.Business.ValidationRules.FluentValidation.AuthValidators
{
    public class MfaCodesValidator : BaseValidator<MfaCodeDto>
    {
        public MfaCodesValidator()
        {
            RuleFor(x => x.UserControlCode).NotEmpty().NotNull();
            // RuleFor(x => x.Type).NotNull();
        }
    }
}
