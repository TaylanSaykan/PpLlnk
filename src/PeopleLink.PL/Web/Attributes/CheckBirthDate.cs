using System.ComponentModel.DataAnnotations;

namespace Web.Attributes
{
    public class CheckBirthDate : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            DateTime? birthDate = (DateTime)value!;
            DateTime minDate = new DateTime(1958, DateTime.Now.Month, DateTime.Now.Day);
            DateTime maxDate = new DateTime(2005, DateTime.Now.Month, DateTime.Now.Day);

            if (birthDate.Value.Year < minDate.Year || birthDate.Value.Year > maxDate.Year)
            {
                return new ValidationResult("Sisteme 18 yaşından küçük veya 65 yaşından büyük çalışan eklenemez!");
            }

            return ValidationResult.Success;
        }
    }
}
