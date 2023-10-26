using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Identity.Pages.Account;

namespace Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
       
        public DashboardController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }
        public async Task<IActionResult> Index()
        {
            var employee = await _userManager.GetUserAsync(User);

            var employeeVm = new Employee.Models.EmployeeViewModel();

            employeeVm.FirstName = employee.FirstName;
            employeeVm.MiddleName = employee.MiddleName;
            employeeVm.LastName = employee.LastName;
            employeeVm.MiddleLastName = employee.MiddleLastName;
            employeeVm.City = employee.CityName;
            employeeVm.District = employee.DistrictName;
            employeeVm.FullAddress = employee.FullAddress;
            employeeVm.Email = employee.Email!;
            employeeVm.PhoneNumber = employee.PhoneNumber!;
            employeeVm.Picture = employee.PictureUri;
            employeeVm.OccupationName = employee.OccupationName;
            employeeVm.DepartmentName = employee.DepartmentName;
            NameMethod(employee);

            return View(employeeVm);
        }

        public async Task<IActionResult> Details()
        {

            var employee = await _userManager.GetUserAsync(User);
            ////int occupationId = employee!.OccupationId;
            ////int departmentId = employee.DepartmentId;
            ////var employeeVm = await _employeeViewModelService.GetEmployeeDetailsViewModelAsync(occupationId, departmentId);
            //if (employeeVm == null)
            //    return NotFound();
            var employeeVm = new EmployeeDetailsViewModel();

            employeeVm.FirstName = employee.FirstName;
            employeeVm.MiddleName = employee.MiddleName;
            employeeVm.LastName = employee.LastName;
            employeeVm.MiddleLastName = employee.MiddleLastName;
            employeeVm.Email = employee.Email!;
            employeeVm.PhoneNumber = employee.PhoneNumber!;
            employeeVm.IdentityNumber = employee.IdentityNumber;
            employeeVm.BirthDate = employee.BirthDate;
            employeeVm.BirthPlace = employee.BirthPlaceName;
            employeeVm.City = employee.CityName;
            employeeVm.District = employee.DistrictName;
            employeeVm.FullAddress = employee.FullAddress;
            employeeVm.Picture = employee.PictureUri;
            employeeVm.HireDate = employee.HireDate;
            employeeVm.TerminationDate = employee.TerminationDate;
            employeeVm.IsActive = employee.IsActive;
            employeeVm.CompanyName = employee.CompanyName;
            employeeVm.OccupationName = employee.OccupationName;
            employeeVm.DepartmentName = employee.DepartmentName;
            NameMethod(employee);


            return View(employeeVm);
        }


        public IActionResult PasswordChange()
        {
            return View();
        }
        private void NameMethod(ApplicationUser? employee)
        {
            TempData["Name"] = employee.FirstName + " " + (employee.MiddleName != null ? employee.MiddleName : "") + (employee.MiddleLastName != null ? employee.MiddleLastName : "") + " " + employee.LastName;
            TempData["Picture"] = employee.PictureUri;
        }

    }
}
