using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.LeaveEntities
{
    public class CompassionateLeave : Leave
    {
        public CompassionateLeave(DateTime startingDate, DateTime endingDate) : base(startingDate, LeaveType.CompassionateLeave, Gender.NotSpecified)
        {
            EndingDate = endingDate;
            TotalDays = (int)((endingDate - startingDate).TotalDays);
        }
    }
}
