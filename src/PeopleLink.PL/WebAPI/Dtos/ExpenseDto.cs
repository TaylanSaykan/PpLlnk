using ApplicationCore.Enums;

namespace WebAPI.Dtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; } = null!;

        public ExpenseType ExpenseType { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public string Status { get; set; } = null!;

        public DateTime RequestDate { get; set; }

        public DateTime? ResponseDate { get; set; }

        public string DocumentUri { get; set; } = null!;
    }
}
