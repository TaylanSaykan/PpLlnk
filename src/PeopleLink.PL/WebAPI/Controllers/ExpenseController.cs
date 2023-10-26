using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseDtoService _expenseDtoService;

        public ExpenseController(IExpenseDtoService expenseDtoService)
        {
            _expenseDtoService = expenseDtoService;
        }

        [HttpGet]
        public async Task<List<ExpenseDto>> GetExpenses(string? status, string employeeId)
        {
            var expenses = await _expenseDtoService.GetAllExpenseDtos(status, employeeId);

            return expenses;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _expenseDtoService.GetExpense(id);

            if (expense == null)
                return NotFound();

            return expense;
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(PostExpenseDto expense)
        {

            if (ModelState.IsValid)
            {
                var createdExpense = await _expenseDtoService.PostExpense(expense);
                return CreatedAtAction(nameof(GetExpense), new { id = createdExpense.Id }, createdExpense);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _expenseDtoService.GetExpense(id);

            if (expense == null)
                return NotFound();

            await _expenseDtoService.DeleteExpenseAsync(id);

            return NoContent();
        }
    }
}
