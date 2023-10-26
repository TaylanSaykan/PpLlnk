using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enums
{
    public enum Currency
    {
        [Display(Name = "₺")]
        TL,
        [Display(Name = "€")]
        Euro,
        [Display(Name = "$")]
        Dolar,
        [Display(Name = "£")]
        Sterlin
    }
}
