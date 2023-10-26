using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using ApplicationCore.Specifications.ExpenseSpecifications;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAPI.Dtos;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class ExpenseDtoService : IExpenseDtoService
    {
        private readonly IExpenseService _expenseService;

        public ExpenseDtoService(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public async Task<List<ExpenseDto>> GetAllExpenseDtos(string? status, string employeeId)
        {
            var specExpenses = new ExpenseStatusFilterSpecification(status, employeeId);
            var expenses = await _expenseService.GetExpensesAsync(specExpenses);

            var expensesvm = expenses.Select(e => new ExpenseDto()
            {
                Id = e.Id,
                EmployeeId = e.EmployeeId,
                ExpenseType = e.ExpenseType,
                Amount = e.Amount,
                Currency = e.Currency,
                Status = e.Status,
                RequestDate = e.RequestDate,
                ResponseDate = e.ResponseDate,
                DocumentUri = e.DocumentUri,
            }).ToList();

            return expensesvm;
        }

        public async Task<Expense> GetExpense(int expenseId)
        {
            var expense = await _expenseService.GetExpenseAsync(expenseId);

            return expense;
        }

        public async Task<Expense> PostExpense(PostExpenseDto model)
        {
            var createdExpense = await _expenseService.CreateExpenseAsync(model.EmployeeId, model.Amount, model.Currency, model.DocumentUri, model.ExpenseType);

            return createdExpense;
        }

        public async Task DeleteExpenseAsync(int expenseId)
        {
            await _expenseService.DeleteAsync(expenseId);
        }

        public async Task UpdateAsync(ExpenseDto expense)
        {
            var updatedExpense = await _expenseService.GetExpenseAsync(expense.Id);
            updatedExpense.Status = expense.Status;
            updatedExpense.ResponseDate = expense.ResponseDate;

            await _expenseService.UpdateAsync(updatedExpense);
        }
    }
}
