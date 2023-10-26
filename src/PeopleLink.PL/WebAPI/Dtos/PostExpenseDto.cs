using ApplicationCore.Enums;

namespace WebAPI.Dtos
{
    public class PostExpenseDto
    {
        public string EmployeeId { get; set; } = null!;

        public ExpenseType ExpenseType { get; set; }

        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        public string DocumentUri { get; set; } = null!;

    }
}
