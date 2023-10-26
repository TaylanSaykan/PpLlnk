using ApplicationCore.Enums;

namespace WebAPI.Dtos
{
    public class PostAdvanceDto
    {
        public string EmployeeId { get; set; } = null!;
        public AdvanceType AdvanceType { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
