using FluentValidation;
using LSP.Entity.DTO.User;

namespace LSP.Business.ValidationRules.FluentValidation.User
{
    public class UserUpdateValidator : AbstractValidator<UpdateUserInformationDto>
    {
        public UserUpdateValidator()
        {

            RuleFor(u => u.Name).NotEmpty().NotNull().MinimumLength(2);
            RuleFor(u => u.Surname).NotEmpty().NotNull().MinimumLength(2);
            RuleFor(u => u.PhoneNumber).NotEmpty().NotNull().MinimumLength(10);


        }
    }
}
