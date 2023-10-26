using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Web.Attributes
{
    public class CheckIdentityNumber : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string idNumber = value!.ToString()!;
            int length = idNumber.Length;
            bool isZero = idNumber[0] == '0';
            bool isNumber = idNumber.All(x => Char.IsNumber(x));

            if (!CheckDigits(idNumber) || length != 11 || isZero || isNumber == false)
            {
                return new ValidationResult("Geçersiz kimlik numarası!");
            }

            return ValidationResult.Success;
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