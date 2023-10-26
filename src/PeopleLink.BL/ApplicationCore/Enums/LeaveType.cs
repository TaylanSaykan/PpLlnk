using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enums
{
    public enum LeaveType
    {
        [Display(Name = "Yıllık İzin")]
        AnnualLeave,
        [Display(Name = "Vefat İzni")]
        BereavementLeave,
        [Display(Name = "Mazeret İzni")]
        CompassionateLeave,
        [Display(Name = "Evlilik İzni")]
        MarriageLeave,
        [Display(Name = "Annelik İzni")]
        MaternityLeave,
        [Display(Name = "Babalık İzni")]
        PaternityLeave,
        [Display(Name = "Hastalık İzni")]
        SickLeave
    }
}
