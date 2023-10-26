using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.LeaveEntities
{
    public class PaternityLeave : Leave
    {
        public PaternityLeave(DateTime startingDate) : base(startingDate, LeaveType.PaternityLeave, Gender.Male)
        {
            EndingDate = startingDate.AddDays(4);
            TotalDays = 5;
        }
    }
}
