using FluentValidation;
using FluentValidation.Validators;
using LSP.Entity.DTO.ValidationRule;

namespace LSP.Business.ValidationRules.FluentValidation
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        public List<ValidationRuleDto> GetValidationRules()
        {
            var rules = new List<ValidationRuleDto>();

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var propertyValidators = this.CreateDescriptor().GetValidatorsForMember(property.Name);

                foreach (var propertyValidator in propertyValidators)
                {
                    var childRules = new List<ValidationRuleDto>();
                    // Check if there is a child validator for the property
                    if (propertyValidator.Validator.GetType().Name.Contains("ChildValidatorAdaptor"))
                    {
                        var childAdaptor = propertyValidator.Validator as IChildValidatorAdaptor;
                        var nestedValidatorType = childAdaptor.GetType().GetProperty("ValidatorType")?.GetValue(childAdaptor) as Type;

                        var nestedValidator = Activator.CreateInstance(nestedValidatorType);

                        var nestedValidatorDescriptor = ((IValidator)nestedValidator).CreateDescriptor();

                        foreach (var rule in nestedValidatorDescriptor.Rules)
                        {
                            var nestedChildRules = new List<ValidationRuleDto>();

                            var childPropertName = rule.PropertyName;
                            foreach (var component in rule.Components)
                            {
                                if (component.Validator.GetType().Name.Contains("ChildValidatorAdaptor"))
                                {
                                    var nestedChildAdaptor = component.Validator as IChildValidatorAdaptor;
                                    nestedChildRules = GetNestedChildRules(nestedChildAdaptor);
                                }
                                childRules.Add(new ValidationRuleDto
                                {
                                    PropertyName = childPropertName,
                                    ValidationExpression = component.Validator.Name,
                                    ErrorMessage = component.Validator.GetDefaultMessageTemplate(null).ToString().Replace("{PropertyName}", childPropertName) ?? null,
                                    ChildProperties = nestedChildRules.Count() > 0 ? nestedChildRules : null
                                });
                            }
                        }
                    }

                    var message = propertyValidator.Options.GetUnformattedErrorMessage().Contains("{PropertyName}") ? propertyValidator.Options.GetUnformattedErrorMessage().Replace("{PropertyName}", property.Name) : propertyValidator.Options.GetUnformattedErrorMessage();

                    rules.Add(new ValidationRuleDto
                    {
                        PropertyName = property.Name,
                        ValidationExpression = propertyValidator.Validator.Name,
                        ErrorMessage = message,
                        ChildProperties = childRules.Count() > 0 ? childRules : null
                    });
                }
            }
            return rules;
        }

        private List<ValidationRuleDto> GetNestedChildRules(IChildValidatorAdaptor childAdaptor)
        {
            var nestedValidatorType = childAdaptor.GetType().GetProperty("ValidatorType")?.GetValue(childAdaptor) as Type;

            var nestedValidator = Activator.CreateInstance(nestedValidatorType);

            var nestedValidatorDescriptor = ((IValidator)nestedValidator).CreateDescriptor();

            var childRules = new List<ValidationRuleDto>();
            foreach (var rule in nestedValidatorDescriptor.Rules)
            {
                var nestedChildRules = new List<ValidationRuleDto>();

                var childPropertName = rule.PropertyName;
                foreach (var component in rule.Components)
                {
                    if (component.Validator.GetType().Name.Contains("ChildValidatorAdaptor"))
                    {
                        var nestedChildAdaptor = component.Validator as IChildValidatorAdaptor;
                        nestedChildRules = GetNestedChildRules(nestedChildAdaptor);
                    }
                    childRules.Add(new ValidationRuleDto
                    {
                        PropertyName = childPropertName,
                        ValidationExpression = component.Validator.Name,
                        ErrorMessage = component.Validator.GetDefaultMessageTemplate(null).ToString().Replace("{PropertyName}", childPropertName) ?? null,
                        ChildProperties = nestedChildRules.Count() > 0 ? nestedChildRules : null
                    });
                }
            }
            return childRules;
        }
    }
}