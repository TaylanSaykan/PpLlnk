namespace Web.Areas.Employee.Models
{
    internal class ExpenseListViewModel
    {
        public List<GetExpenseViewModel> Expenses { get; set; } = new();
    }
}