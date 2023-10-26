using ApplicationCore.Enums;

namespace WebAPI.Dtos
{
    public class PostLeaveDto
    {
        public string EmployeeId { get; set; } = null!;

        public DateTime StartingDate { get; set; }

        public DateTime EndingDate { get; set; }

        public LeaveType LeaveType { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public int TotalDays { get; set; }

        public string Status { get; set; } = null!;
    }
}
