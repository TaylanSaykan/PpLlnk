using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enums
{
    public enum ExpenseType    {
        [Display(Name = "Konaklama")]
        Accommodation,
        [Display(Name = "Özel Sağlık")]
        HealthInsuranceAndOtherBenefits,
        [Display(Name = "Yiyecek")]
        Meal,
        [Display(Name = "Telefon ve İnternet")]
        PhoneAndInternet,
        [Display(Name = "Kişisel Gelişim")]
        ProfessionalDevelopment,
        [Display(Name = "Seyahat")]
        Transportation
    }
}
