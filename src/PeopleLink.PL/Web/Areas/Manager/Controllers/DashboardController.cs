using ApplicationCore.Entities.CityAndDistrictEntities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Identity.Pages.Account;

namespace Web.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICityService _cityService;

        public DashboardController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ICityService cityService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cityService = cityService;
        }
        public async Task<IActionResult> Index()
        {
            var manager = await _userManager.GetUserAsync(User);
            var managerVm = new ManagerViewModel();
            managerVm.FirstName = manager.FirstName;
            managerVm.MiddleName = manager.MiddleName;
            managerVm.LastName = manager.LastName;
            managerVm.MiddleLastName = manager.MiddleLastName;
            managerVm.City = manager.CityName;
            managerVm.District = manager.DistrictName;
            managerVm.FullAddress = manager.FullAddress;
            managerVm.Email = manager.Email!;
            managerVm.PhoneNumber = manager.PhoneNumber!;
            managerVm.Picture = manager.PictureUri;
            managerVm.OccupationName = manager.OccupationName;
            managerVm.DepartmentName = manager.DepartmentName;
            NameMethod(manager);

            return View(managerVm);
        }



        public async Task<IActionResult> Details()
        {
            var manager = await _userManager.GetUserAsync(User);

            TempData["Name"] = manager!.FirstName + " " + (manager.MiddleName != null ? manager.MiddleName : "") + (manager.MiddleLastName != null ? manager.MiddleLastName : "") + " " + manager.LastName;
            TempData["Picture"] = manager.PictureUri;

            var managerVm = new ManagerDetailsViewModel();
            managerVm.FirstName = manager.FirstName;
            managerVm.MiddleName = manager.MiddleName;
            managerVm.LastName = manager.LastName;
            managerVm.MiddleLastName = manager.MiddleLastName;
            managerVm.Email = manager.Email!;
            managerVm.PhoneNumber = manager.PhoneNumber!;
            managerVm.IdentityNumber = manager.IdentityNumber;
            managerVm.BirthDate = manager.BirthDate;
            managerVm.BirthPlace = manager.BirthPlaceName;
            managerVm.City = manager.CityName;
            managerVm.District = manager.DistrictName;
            managerVm.FullAddress = manager.FullAddress;
            managerVm.Picture = manager.PictureUri;
            managerVm.HireDate = manager.HireDate;
            managerVm.TerminationDate = manager.TerminationDate;
            managerVm.IsActive = manager.IsActive;
            managerVm.CompanyName = manager.CompanyName;
            managerVm.OccupationName = manager.OccupationName;
            managerVm.DepartmentName = manager.DepartmentName;
            NameMethod(manager);

            return View(managerVm);
        }

        public async Task<JsonResult> GetDistricts(int cityId)
        {
            var city = await _cityService.GetCityAsync(cityId);
            var districts = city.Districts.Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

            return new JsonResult(districts);
        }
        private void NameMethod(ApplicationUser? manager)
        {
            TempData["Name"] = manager.FirstName + " " + (manager.MiddleName != null ? manager.MiddleName : "") + (manager.MiddleLastName != null ? manager.MiddleLastName : "") + " " + manager.LastName;
            TempData["Picture"] = manager.PictureUri;
        }
    }
}
