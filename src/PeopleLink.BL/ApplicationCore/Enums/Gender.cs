using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enums
{
    public enum Gender
    {
        [Display(Name = "Erkek")]
        Male,
        [Display(Name = "Kadın")]
        Female,
        [Display(Name = "Diğer")]
        Other,
        [Display(Name = "Belirtilmemiş")]
        NotSpecified
    }
}
