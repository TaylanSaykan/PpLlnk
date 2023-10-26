using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using Web.Areas.Admin.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Attributes
{
    public class CheckMersisNumber : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.DisplayName} alanı boş bırakılamaz!");
            }

            string? mersisNumber = value as string;

           
            bool firstDigit = mersisNumber[0] == '0';
            int length = mersisNumber.Length;
            string lastThreeDigit = mersisNumber.Substring(mersisNumber.Length - 5, 3); // 11.,12.,13. digit 0 olmalı
            string lastTwoDigit = mersisNumber.Substring(mersisNumber.Length - 2);  // son 2 hane 00,15,16,17,18,19 olmalı
            string[] lastTwo = { "00", "15", "16", "17", "18", "19" };
            bool isNumber = mersisNumber.All(x => Char.IsNumber(x));
            string firstEleven = mersisNumber.Substring(0, 11);  // sahıs - tc no
            string firstTenButOne = mersisNumber.Substring(1, 10); // diger - vergi no

            if (validationContext.ObjectInstance is PostCompanyViewModel postCompanyModel)
            {
                var companyModel = (PostCompanyViewModel)validationContext.ObjectInstance;
                CompanyTitle title = companyModel.Title;
                string taxNumber = companyModel.TaxNumber;

                if (title == CompanyTitle.Sahis)
                {
                    if (length != 16 || !isNumber || firstEleven != taxNumber || lastThreeDigit != "000" || !lastTwo.Contains(lastTwoDigit))
                    {
                        return new ValidationResult("Geçersiz MERSİS numarası!");
                    }

                    return ValidationResult.Success;
                }
                else
                {
                    if (length != 16 || !isNumber || !firstDigit || firstTenButOne != taxNumber || lastThreeDigit != "000" || !lastTwo.Contains(lastTwoDigit))
                    {
                        return new ValidationResult("Geçersiz MERSİS numarası!");
                    }

                    return ValidationResult.Success;
                }
            }
            else
            {
                var companyModel = (PutCompanyViewModel)validationContext.ObjectInstance;
                CompanyTitle title = companyModel.Title;
                string taxNumber = companyModel.TaxNumber;
                if (title == CompanyTitle.Sahis)
                {
                    if (length != 16 || !isNumber || firstEleven != taxNumber || lastThreeDigit != "000" || !lastTwo.Contains(lastTwoDigit))
                    {
                        return new ValidationResult("Geçersiz MERSİS numarası!");
                    }

                    return ValidationResult.Success;
                }
                else
                {
                    if (length != 16 || !isNumber || !firstDigit || firstTenButOne != taxNumber || lastThreeDigit != "000" || !lastTwo.Contains(lastTwoDigit))
                    {
                        return new ValidationResult("Geçersiz MERSİS numarası!");
                    }

                    return ValidationResult.Success;
                }
            }

        }
    }
}
