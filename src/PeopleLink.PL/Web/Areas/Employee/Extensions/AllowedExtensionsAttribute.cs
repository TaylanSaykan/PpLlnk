using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Employee.Extensions
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!_extensions.Contains(fileExtension))
                {
                    return new ValidationResult($"Geçersiz dosya uzantısı. Geçerli uzantılar: {string.Join(", ", _extensions)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
