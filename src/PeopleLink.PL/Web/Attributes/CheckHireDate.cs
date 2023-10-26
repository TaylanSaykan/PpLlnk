using System.ComponentModel.DataAnnotations;

namespace Web.Attributes
{
    public class CheckHireDate : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime? hireDate = (DateTime)value!;

            if (hireDate.Value < DateTime.Now.AddDays(-7) || hireDate.Value > DateTime.Now.AddDays(7))
            {
                return new ValidationResult("İşe giriş tarihi geçersiz.");
            }

            return ValidationResult.Success;
        }
    }
}
