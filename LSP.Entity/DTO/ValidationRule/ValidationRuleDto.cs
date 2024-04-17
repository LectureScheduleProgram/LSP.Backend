namespace LSP.Entity.DTO.ValidationRule
{
    public class ValidationRuleDto
    {
        public string PropertyName { get; set; }

        public string ValidationExpression { get; set; }

        public string ErrorMessage { get; set; }
        public List<ValidationRuleDto> ChildProperties { get; set; }
    }
}
