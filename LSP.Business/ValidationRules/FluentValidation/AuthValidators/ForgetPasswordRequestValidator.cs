using FluentValidation;
using LSP.Core.Extensions;
using LSP.Core.Utilities.Constants;
using LSP.Entity.DTO.Authentication;

namespace LSP.Business.ValidationRules.FluentValidation.AuthValidators
{
    public class ForgetPasswordRequestValidator : BaseValidator<ForgetPasswordRequestDto>
    {
        public ForgetPasswordRequestValidator()
        {
            RuleFor(p => p.MfaTypes).NotNull().NotEmpty();
            //RuleForEach(p => p.SecuritySystems).SetValidator(new SecuritiesRequestValidator());

            RuleFor(p => p.UserControlCode).NotEmpty().NotNull();
            RuleFor(p => p.NewPassword)
                .Must(p => !PasswordRules.ContainsTurkishCharacters(p))
                .WithMessage(AspectMessages.cant_contain_turkish_characters);

            RuleFor(p => p.NewPassword).Must(PasswordRules.GreaterThan8).WithMessage(AspectMessages.MinimumPasswordEight);
            RuleFor(p => p.NewPassword).Must(PasswordRules.NotStartWith("0")).WithMessage(AspectMessages.cant_start_with_zero);
            RuleFor(p => p.NewPassword).Must(PasswordRules.NotStartWith("19")).WithMessage(AspectMessages.cant_start_with_nineteen);
            RuleFor(p => p.NewPassword).Must(PasswordRules.NotStartWith("20")).WithMessage(AspectMessages.cant_start_with_twenty);
            RuleFor(p => p.NewPassword).Must(PasswordRules.NotContainRepeatingCharacters).WithMessage(AspectMessages.cant_contain_repeating_characters);
            RuleFor(p => p.NewPassword).Must(PasswordRules.NotContainFourSequentialNumbers).WithMessage(AspectMessages.cant_contain_four_sequential_numbers);
        }

    }
}
