using ApplicationCore.Enums;
using System;

namespace ApplicationCore.Entities
{
    //deneme
    public abstract class Advance : BaseEntity
    {
        public Advance(AdvanceType advanceType)
        {
            AdvanceType = advanceType;
        }
        public string EmployeeId { get; set; } = null!;
        public AdvanceType AdvanceType { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string Status { get; set; } = ApprovalStatus.WaitingApproved;
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? ResponseDate { get; set; }
        public string Description { get; set; } = null!;

        //yorum
    }
}
