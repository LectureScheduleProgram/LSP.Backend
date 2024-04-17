using FluentValidation;
using LSP.Core.Utilities.Constants;
using LSP.Entity.DTO.Authentication;

namespace LSP.Business.ValidationRules.FluentValidation.AuthValidators
{
    public class RegisterValidator : BaseValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage(AspectMessages.InvalidEmail);
            RuleFor(p => p.Password).NotEmpty().NotNull();
            RuleFor(p => p.Password)
                .Must(p => !PasswordRules.ContainsTurkishCharacters(p))
                .WithMessage(AspectMessages.cant_contain_turkish_characters);

            RuleFor(p => p.Password).Must(PasswordRules.GreaterThan8).WithMessage(AspectMessages.MinimumPasswordEight);
            // RuleFor(p => p.Password).Must(PasswordRules.NotStartWith("0")).WithMessage(AspectMessages.cant_start_with_zero);
            //  RuleFor(p => p.Password).Must(PasswordRules.NotStartWith("19")).WithMessage(AspectMessages.cant_start_with_nineteen);
            //   RuleFor(p => p.Password).Must(PasswordRules.NotStartWith("20")).WithMessage(AspectMessages.cant_start_with_twenty);
            // RuleFor(p => p.Password).Must(PasswordRules.NotContainRepeatingCharacters).WithMessage(AspectMessages.cant_contain_repeating_characters);
            //   RuleFor(p => p.Password).Must(PasswordRules.NotContainFourSequentialNumbers).WithMessage(AspectMessages.cant_contain_four_sequential_numbers);
        }

    }
}