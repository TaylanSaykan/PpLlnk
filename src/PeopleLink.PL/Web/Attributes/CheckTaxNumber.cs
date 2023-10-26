using System.ComponentModel.DataAnnotations;
using Web.Areas.Admin.Models;

namespace Web.Attributes
{
    public class CheckTaxNumber : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult($"{validationContext.DisplayName} alanı boş bırakılamaz!");
            }

            string? taxNumber = value.ToString();

            int length = taxNumber.Length;
            bool isZero = taxNumber[0] == '0';
            bool isNumber = taxNumber.All(x => Char.IsNumber(x));

            if (validationContext.ObjectInstance is PostCompanyViewModel postCompanyModel)
            {
                var companyModel = (PostCompanyViewModel)validationContext.ObjectInstance;
                var title = companyModel.Title;

                if (title == CompanyTitle.Sahis)
                {
                    if (!CheckDigits(taxNumber) || length != 11 || isZero || isNumber == false)
                    {
                        return new ValidationResult("Geçersiz vergi numarası!");
                    }
                }
                else
                {
                    if (length != 10 || isNumber == false)
                    {
                        return new ValidationResult("Geçersiz vergi numarası!");
                    }
                }

                return ValidationResult.Success;
            }
            else
            {
                var companyModel = (PutCompanyViewModel)validationContext.ObjectInstance;
                var title = companyModel.Title;

                if (title == CompanyTitle.Sahis)
                {
                    if (!CheckDigits(taxNumber) || length != 11 || isZero || isNumber == false)
                    {
                        return new ValidationResult("Geçersiz vergi numarası!");
                    }
                }
                else
                {
                    if (length != 10 || isNumber == false)
                    {
                        return new ValidationResult("Geçersiz vergi numarası!");
                    }
                }

                return ValidationResult.Success;
            }

        }
        private bool CheckDigits(string id)
        {
            int oddDigitsTotal = 0;
            int evenDigitsTotal = 0;

            for (int i = 0; i < id.Length - 2; i++)
            {
                if (i % 2 == 0)
                {
                    oddDigitsTotal += Convert.ToInt32(id[i].ToString());
                }
                else
                {
                    evenDigitsTotal += Convert.ToInt32(id[i].ToString());
                }
            }

            int tenthDigit = (oddDigitsTotal * 7 - evenDigitsTotal) % 10;
            int eleventhDigit = (oddDigitsTotal + evenDigitsTotal + Convert.ToInt32(id[9].ToString())) % 10;

            return (tenthDigit.ToString() == id[9].ToString()) && (eleventhDigit.ToString() == id[10].ToString());
        }
    }
}
