using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.LeaveEntities
{
    public class BereavementLeave : Leave
    {
        public BereavementLeave(DateTime startingDate) : base(startingDate, LeaveType.BereavementLeave, Gender.NotSpecified)
        {
            EndingDate = startingDate.AddDays(2);
            TotalDays = 3;
        }
    }
}
