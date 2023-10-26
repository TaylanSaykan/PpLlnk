using ApplicationCore.Entities;
using ApplicationCore.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Employee.Models;

namespace Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class ExpenseController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExpenseController(HttpClient httpClient, UserManager<ApplicationUser> userManager)
        {
            _httpClient = httpClient;
            _userManager = userManager;
        }
        public async Task<IActionResult> AllExpenses(string? status)
        {
            var user = await _userManager.GetUserAsync(User);
            var employeeId = _userManager.GetUserId(User);
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            string apiUrl = $"https://peoplelinkapi1.azurewebsites.net/api/Expense?employeeId={employeeId}";
            string apiUrlWithStatus = $"https://peoplelinkapi1.azurewebsites.net/api/Expense?status={status}&employeeId={employeeId}";
            NameMethod(user);

            ViewBag.Status = status;

            if (status != null)
            {
                var expensesWithStatus = await _httpClient.GetFromJsonAsync<List<GetExpenseViewModel>>(apiUrlWithStatus);
                return View(expensesWithStatus);

            }

            var expenses = await _httpClient.GetFromJsonAsync<List<GetExpenseViewModel>>(apiUrl);
            return View(expenses);
        }

        public async Task<IActionResult> ExpenseRequest()
        {
            var user = await _userManager.GetUserAsync(User);
            var expensesTypes = Enum.GetValues(typeof(ExpenseType)).Cast<ExpenseType>().ToList();
            var currencyTypes = Enum.GetValues(typeof(Currency)).Cast<Currency>().ToList();
            ViewBag.ExpenseTypes = expensesTypes;
            ViewBag.CurrentTypes = currencyTypes;
            ViewData["EmployeeId"] = user!.Id;
            ViewData["Amount"] = user.ExpenseAllowance;
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            NameMethod(user);
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ExpenseRequest(PostExpenseViewModel expenseViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            string apiUrl = $"https://peoplelinkapi1.azurewebsites.net/api/Expense?&employeeId={user.Id}";
            var expenses = await _httpClient.GetFromJsonAsync<List<GetExpenseViewModel>>(apiUrl);
            var expensesTypes = Enum.GetValues(typeof(ExpenseType)).Cast<ExpenseType>().ToList();
            var currencyTypes = Enum.GetValues(typeof(Currency)).Cast<Currency>().ToList();
            ViewBag.ExpenseTypes = expensesTypes;
            ViewBag.CurrentTypes = currencyTypes;
            ViewData["Amount"] = user.ExpenseAllowance;
            NameMethod(user);

            if (ModelState.IsValid)
            {
                if (expenses!.Any(l => l.Status == expenseViewModel.Status && l.ExpenseType == expenseViewModel.ExpenseType))
                {
                    TempData["HasPendingExpense"] = "Onay bekleyen harcama talebiniz bulunmaktadır.";
                    return RedirectToAction("AllExpenses");
                }
                else
                {
                    var expenseAmount = expenseViewModel.Currency == Currency.TL ? expenseViewModel.Amount
   : (expenseViewModel.Currency == Currency.Dolar ? expenseViewModel.Amount * 27
   : (expenseViewModel.Currency == Currency.Euro ? expenseViewModel.Amount * 29 : expenseViewModel.Amount * 33));

                    if (user.ExpenseAllowance == 0)
                    {
                        TempData["PendingExpense"] = "Harcama talep edebileceğiniz tutar bitmiştir.";
                        return RedirectToAction("AllExpenses");
                    }
                    else if (user.ExpenseAllowance < expenseAmount)
                    {
                        TempData["Amount"] = "Harcama talep ettiğiniz tutar harcama hakkınızdan fazladır.";
                        return RedirectToAction("AllExpenses");
                    }
                    else
                    {
                        string ext = Path.GetExtension(expenseViewModel.Document.FileName);
                        string fileName = Path.Combine(Guid.NewGuid().ToString() + ext);
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/document", fileName);

                        using (var fileStream = System.IO.File.Create(filePath))
                        {
                            expenseViewModel.Document.CopyTo(fileStream);
                        }

                        var expense = new AddExpenseViewModel()
                        {
                            EmployeeId = expenseViewModel.EmployeeId,
                            Currency = expenseViewModel.Currency,
                            Amount = expenseViewModel.Amount,
                            ExpenseType = expenseViewModel.ExpenseType,
                            DocumentUri = fileName
                        };

                        user.ExpenseAllowance = user.ExpenseAllowance - expenseAmount;
                        await _httpClient.PostAsJsonAsync("https://peoplelinkapi1.azurewebsites.net/api/Expense/", expense);
                        await _userManager.UpdateAsync(user);

                        ViewData["Amount"] = user.ExpenseAllowance;
                        TempData["SuccessExpense"] = "Harcama talebi başarıyla oluşturuldu.";
                        return RedirectToAction("AllExpenses");
                    }

                }
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {

            var expense = await _httpClient.GetFromJsonAsync<GetExpenseViewModel>($"https://peoplelinkapi1.azurewebsites.net/api/Expense/{expenseId}");

            var user = await _userManager.GetUserAsync(User);
            NameMethod(user);

            var expenseAmount = expense.Currency == Currency.TL ? expense.Amount
    : (expense.Currency == Currency.Dolar ? expense.Amount * 27
    : (expense.Currency == Currency.Euro ? expense.Amount * 29 : expense.Amount * 33));

            user.ExpenseAllowance = user.ExpenseAllowance + expenseAmount;
            await _userManager.UpdateAsync(user);

            await _httpClient.DeleteAsync($"https://peoplelinkapi1.azurewebsites.net/api/Expense/{expenseId}");
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/document", expense!.DocumentUri);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            };

            TempData["DeleteSuccessExpense"] = "Harcama talebi başarıyla silindi.";
            return RedirectToAction("AllExpenses");
        }
        private void NameMethod(ApplicationUser? user)
        {
            TempData["Name"] = user.FirstName + " " + (user.MiddleName != null ? user.MiddleName : "") + (user.MiddleLastName != null ? user.MiddleLastName : "") + " " + user.LastName;
            TempData["Picture"] = user.PictureUri;
        }
    }
}
