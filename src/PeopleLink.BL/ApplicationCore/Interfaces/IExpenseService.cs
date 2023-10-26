using ApplicationCore.Entities;
using ApplicationCore.Enums;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetExpensesAsync();

        Task<List<Expense>> GetExpensesAsync(ISpecification<Expense> specification);

        Task<Expense> GetExpenseAsync(int id);

        Task<Expense> CreateExpenseAsync(string employeeId, decimal amount, Currency currency, string documentUri, ExpenseType type);

        Task DeleteAsync(int id);

        Task UpdateAsync(Expense expense);
    }
}
