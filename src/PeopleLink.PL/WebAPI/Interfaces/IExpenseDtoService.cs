using WebAPI.Dtos;

namespace WebAPI.Interfaces
{
    public interface IExpenseDtoService
    {
        Task<List<ExpenseDto>> GetAllExpenseDtos(string? status, string employeeId);

        Task<Expense> GetExpense(int expenseId);

        Task<Expense> PostExpense(PostExpenseDto model);

        Task DeleteExpenseAsync(int expenseId);

        Task UpdateAsync(ExpenseDto expense);
    }
}
