using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using System.Drawing;
using System.Net.Http;

namespace Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClient _httpClient;

        public EmployeeController(UserManager<ApplicationUser> userManager, HttpClient httpClient)
        {
            _userManager = userManager;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> AllEmployees()
        {
            var employee = await _userManager.GetUserAsync(User);
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            NameMethod(employee);

            return View(employees);
        }

        public async Task<IActionResult> DetailEmployee(string employeeId)
        {
            var employee = await _userManager.FindByIdAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            NameMethod(employee);
            return View(employee);
        }

        public async Task<IActionResult> LeaveRequests(string? status)
        {
            var emp = await _userManager.GetUserAsync(User);
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var employeeLeaves = new LeaveListViewModel();
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            TempData["Name"] = emp.FirstName + " " + (emp.MiddleName != null ? emp.MiddleName : "") + (emp.MiddleLastName != null ? emp.MiddleLastName : "") + " " + emp.LastName;
            TempData["Picture"] = emp.PictureUri;


            if (status != null)
            {
                foreach (var employee in employees)
                {
                    var userId = employee.Id;

                    string apiUrlWithStatus = $"https://peoplelinkapi1.azurewebsites.net/api/Leave?approvalStatus={status}&employeeId={userId}";
                    var leaveWithStatus = await _httpClient.GetFromJsonAsync<List<LeaveViewModel>>(apiUrlWithStatus);

                    employeeLeaves.Leaves.AddRange(leaveWithStatus);
                    ViewBag.Status = status;
                }

                return View(employeeLeaves);
            }
            else
            {
                foreach (var employee in employees)
                {
                    var userId = employee.Id;
                    string apiUrl = $"https://peoplelinkapi1.azurewebsites.net/api/Leave?employeeId={userId}";
                    var leaves = await _httpClient.GetFromJsonAsync<List<LeaveViewModel>>(apiUrl);
                    employeeLeaves.Leaves.AddRange(leaves);
                    ViewBag.Status = status;
                }
                return View(employeeLeaves); // kişiler ve izinleri dönmeli List<LeaveViewModel>
            }

        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLeave(LeaveViewModel leave)
        {
            await _httpClient.PutAsJsonAsync("https://peoplelinkapi1.azurewebsites.net/api/Leave/" + leave.Id, leave);
            TempData["Success"] = $"Başarıyla {leave.Status}";
            return RedirectToAction(nameof(LeaveRequests));
        }

        public async Task<IActionResult> UpdateLeave(int leaveId, string status)
        {
            var leave = await _httpClient.GetFromJsonAsync<LeaveViewModel>($"https://peoplelinkapi1.azurewebsites.net/api/Leave/{leaveId}");

            leave.Status = status;
            leave.ApprovalDate = DateTime.Now;
            return View(leave);
        }

        public async Task<IActionResult> ExpenseRequests(string? status)
        {
            var emp = await _userManager.GetUserAsync(User);
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var employeeExpenses = new ExpenseListViewModel();
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            TempData["Name"] = emp.FirstName + " " + (emp.MiddleName != null ? emp.MiddleName : "") + (emp.MiddleLastName != null ? emp.MiddleLastName : "") + " " + emp.LastName;
            TempData["Picture"] = emp.PictureUri;

            if (status != null)
            {
                foreach (var employee in employees)
                {
                    var userId = employee.Id;
                    string apiUrlWithStatus = $"https://peoplelinkapi1.azurewebsites.net/api/Expense?status={status}&employeeId={userId}";
                    var expenseWithStatus = await _httpClient.GetFromJsonAsync<List<GetExpenseViewModel>>(apiUrlWithStatus);
                    employeeExpenses.Expenses.AddRange(expenseWithStatus);
                    ViewBag.Status = status;
                }

                return View(employeeExpenses);
            }
            else
            {
                foreach (var employee in employees)
                {
                    var userId = employee.Id;
                    string apiUrl = $"https://peoplelinkapi1.azurewebsites.net/api/Expense?employeeId={userId}";
                    var expenses = await _httpClient.GetFromJsonAsync<List<GetExpenseViewModel>>(apiUrl);
                    employeeExpenses.Expenses.AddRange(expenses);
                    ViewBag.Status = status;
                }
                return View(employeeExpenses); // kişiler ve izinleri dönmeli List<LeaveViewModel>
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateExpense(GetExpenseViewModel expense)
        {
            await _httpClient.PutAsJsonAsync("https://peoplelinkapi1.azurewebsites.net/api/Expense/" + expense.Id, expense);
            TempData["Success"] = $"Başarıyla {expense.Status}";
            return RedirectToAction(nameof(ExpenseRequests));
        }

        public async Task<IActionResult> UpdateExpense(int expenseId, string status)
        {
            var expense = await _httpClient.GetFromJsonAsync<GetExpenseViewModel>($"https://peoplelinkapi1.azurewebsites.net/api/Expense/{expenseId}");

            expense.Status = status;
            expense.ResponseDate = DateTime.Now;
            return View(expense);
        }

        public async Task<IActionResult> AdvanceRequests(string? status)
        {
            var emp = await _userManager.GetUserAsync(User);
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var employeeAdvances = new AdvanceListViewModel();
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            TempData["Name"] = emp.FirstName + " " + (emp.MiddleName != null ? emp.MiddleName : "") + (emp.MiddleLastName != null ? emp.MiddleLastName : "") + " " + emp.LastName;
            TempData["Picture"] = emp.PictureUri;

            if (status != null)
            {
                foreach (var employee in employees)
                {
                    var userId = employee.Id;
                    string apiUrlWithStatus = $"https://peoplelinkapi1.azurewebsites.net/api/Advance?status={status}&employeeId={userId}";
                    var advanceWithStatus = await _httpClient.GetFromJsonAsync<List<AdvanceViewModel>>(apiUrlWithStatus);
                    employeeAdvances.Advances.AddRange(advanceWithStatus);
                    ViewBag.Status = status;
                }

                return View(employeeAdvances);
            }
            else
            {
                foreach (var employee in employees)
                {
                    var userId = employee.Id;
                    string apiUrl = $"https://peoplelinkapi1.azurewebsites.net/api/Advance?employeeId={userId}";
                    var advances = await _httpClient.GetFromJsonAsync<List<AdvanceViewModel>>(apiUrl);
                    employeeAdvances.Advances.AddRange(advances);
                    ViewBag.Status = status;
                }
                return View(employeeAdvances); // kişiler ve izinleri dönmeli List<LeaveViewModel>
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAdvance(AdvanceViewModel advance)
        {
            await _httpClient.PutAsJsonAsync("https://peoplelinkapi1.azurewebsites.net/api/Advance/" + advance.Id, advance);
            TempData["Success"] = $"Başarıyla {advance.Status}";
            return RedirectToAction(nameof(AdvanceRequests));
        }

        public async Task<IActionResult> UpdateAdvance(int advanceId, string status)
        {
            var advance = await _httpClient.GetFromJsonAsync<AdvanceViewModel>($"https://peoplelinkapi1.azurewebsites.net/api/Advance/{advanceId}");

            advance.Status = status;
            advance.ResponseDate = DateTime.Now;
            return View(advance);

        }
        private void NameMethod(ApplicationUser? employee)
        {
            TempData["Name"] = employee.FirstName + " " + (employee.MiddleName != null ? employee.MiddleName : "") + (employee.MiddleLastName != null ? employee.MiddleLastName : "") + " " + employee.LastName;
            TempData["Picture"] = employee.PictureUri;
        }

    }
}
