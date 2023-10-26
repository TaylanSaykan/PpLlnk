namespace Web.Areas.Employee.Models
{
    public class AddExpenseViewModel
    {
        public string EmployeeId { get; set; } = null!;
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public string DocumentUri { get; set; } = null!;
    }
}
