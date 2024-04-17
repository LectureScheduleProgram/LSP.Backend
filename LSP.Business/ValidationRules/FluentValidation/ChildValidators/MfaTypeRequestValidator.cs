using FluentValidation;
using LSP.Core.Extensions;
using LSP.Core.Utilities.Constants;
using LSP.Entity.DTO.Authentication;

namespace LSP.Business.ValidationRules.FluentValidation.ChildValidators
{
    public class MfaTypeRequestValidator : AbstractValidator<MfaTypeRequestDto>
    {
        public MfaTypeRequestValidator()
        {
            RuleFor(mfaType => mfaType.SecurityType.EnumToString())
                .NotNull().NotEmpty().WithMessage(AspectMessages.invalid_security_type)
                .Must(type => !type.Any(char.IsDigit)).WithMessage(AspectMessages.invalid_security_type);

            RuleFor(mfaType => mfaType.SecurityCode).NotNull().NotEmpty().WithMessage(AspectMessages.invalid_security_code);
        }
    }
}