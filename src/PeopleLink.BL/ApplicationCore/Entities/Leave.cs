using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities
{
    public abstract class Leave : BaseEntity
    {
        public Leave(DateTime startingDate, LeaveType leaveType, Gender genderType)
        {
            StartingDate = startingDate;
            LeaveType = leaveType;
            Gender = genderType;
        }

        public string EmployeeId { get; set; } = null!;

        public LeaveType LeaveType { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime EndingDate { get; set; }

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public DateTime? ApprovalDate { get; set; }

        public int TotalDays { get; set; }

        public Gender Gender { get; set; }

        public string Status { get; set; } = ApprovalStatus.WaitingApproved;
    }
}
