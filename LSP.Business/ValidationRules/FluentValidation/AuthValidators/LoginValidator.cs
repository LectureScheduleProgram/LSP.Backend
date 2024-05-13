using FluentValidation;
using LSP.Core.Utilities.Constants;
using LSP.Entity.DTO;

namespace LSP.Business.ValidationRules.FluentValidation.AuthValidators
{
    public class LoginValidator : BaseValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage(AspectMessages.InvalidEmail);
            RuleFor(p => p.Password).NotEmpty();
        }
    }
}