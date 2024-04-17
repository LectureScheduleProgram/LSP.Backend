using FluentValidation;
using LSP.Business.ValidationRules.FluentValidation.ChildValidators;
using LSP.Entity.DTO.Authentication;

namespace LSP.Business.ValidationRules.FluentValidation.AuthValidators
{
	public class SecuritiesRequestValidator : BaseValidator<SecurityWithUserControlRequestDto>
	{
		public SecuritiesRequestValidator()
		{
			RuleFor(p => p.UserControlCode).NotEmpty().NotNull();
			RuleForEach(p => p.MfaTypes).SetValidator(new MfaTypeRequestValidator());
		}
	}
}
