using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enums
{
    public enum CompanyTitle
    {
        [Display(Name = "Anonim")]
        Anonim,
        [Display(Name = "Limited")]
        Limited,
        [Display(Name = "Şahıs")]
        Sahis
    }
}
