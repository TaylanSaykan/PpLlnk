using ApplicationCore.Entities;
using ApplicationCore.Entities.ExpenseEntities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IRepository<Expense> _expenseRepo;

        public ExpenseService(IRepository<Expense> expenseRepo)
        {
            _expenseRepo = expenseRepo;
        }

        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await _expenseRepo.GetAllAsync();
        }

        public async Task<List<Expense>> GetExpensesAsync(ISpecification<Expense> specification)
        {
            return await _expenseRepo.GetAllAsync(specification);
        }

        public async Task<Expense> GetExpenseAsync(int id)
        {
            var expense = await _expenseRepo.GetByIdAsync(id);

            return expense!;
        }

        public async Task<Expense> CreateExpenseAsync(string employeeId, decimal amount, Currency currency, string documentUri, ExpenseType type)
        {
            Expense expense;

            switch (type)
            {
                case ExpenseType.Accommodation:
                    expense = new AccommodationExpense();
                    break;
                case ExpenseType.HealthInsuranceAndOtherBenefits:
                    expense = new HealtInsuranceAndOtherBenefitsExpense();
                    break;
                case ExpenseType.Meal:
                    expense = new MealExpense();
                    break;
                case ExpenseType.PhoneAndInternet:
                    expense = new PhoneAndInternetExpense();
                    break;
                case ExpenseType.ProfessionalDevelopment:
                    expense = new ProfessionalDevelopmentExpense();
                    break;
                case ExpenseType.Transportation:
                    expense = new TransportationExpense();
                    break;
                default:
                    throw new NotImplementedException();
            };

            expense.EmployeeId = employeeId;
            expense.Amount = amount;
            expense.Currency = currency;
            expense.DocumentUri = documentUri;

            return await _expenseRepo.AddAsync(expense);
        }

        public async Task DeleteAsync(int expenseId)
        {
            var expense = await _expenseRepo.GetByIdAsync(expenseId);

            await _expenseRepo.DeleteAsync(expense!);
        }

        public async Task UpdateAsync(Expense expense)
        {
            var updatedExpense = await _expenseRepo.GetByIdAsync(expense.Id);

            await _expenseRepo.UpdateAsync(updatedExpense);
        }
    }
}
