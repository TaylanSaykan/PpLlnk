using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.LeaveEntities
{
    public class MaternityLeave : Leave
    {
        public MaternityLeave(DateTime startingDate) : base(startingDate, LeaveType.MaternityLeave, Gender.Female)
        {
            EndingDate = startingDate.AddDays(111);
            TotalDays = 112;
        }
    }
}
