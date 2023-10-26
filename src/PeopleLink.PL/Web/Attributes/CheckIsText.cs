using System.ComponentModel.DataAnnotations;

namespace Web.Attributes
{
    public class CheckIsText : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string? text = value.ToString();

                if (text.Any(x => Char.IsNumber(x)))
                {
                    return new ValidationResult($"{validationContext.DisplayName} içinde rakam olamaz!");
                }
            }

            return ValidationResult.Success;
        }
    }
}
