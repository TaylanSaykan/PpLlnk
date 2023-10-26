using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICityService _cityService;

        public DashboardController(UserManager<ApplicationUser> userManager, ICityService cityService)
        {
            _userManager = userManager;
            _cityService = cityService;
        }

        public async Task<IActionResult> Index()
        {
            var admin = await _userManager.GetUserAsync(User);

            var adminvm = new AdminViewModel();
            adminvm.FirstName = admin!.FirstName;
            adminvm.MiddleName = admin.MiddleName;
            adminvm.LastName = admin.LastName;
            adminvm.MiddleLastName = admin.MiddleLastName;
            adminvm.City = admin.CityName;
            adminvm.District = admin.DistrictName;
            adminvm.FullAddress = admin.FullAddress;
            adminvm.Email = admin.Email!;
            adminvm.PhoneNumber = admin.PhoneNumber!;
            adminvm.PictureUri = admin.PictureUri;
            adminvm.OccupationName = admin.OccupationName;
            adminvm.DepartmentName = admin.DepartmentName;
            NameMethod(admin);

            return View(adminvm);
        }



        public async Task<IActionResult> Details()
        {
            var admin = await _userManager.GetUserAsync(User);


            var adminvm = new AdminDetailsViewModel();
            adminvm.FirstName = admin.FirstName;
            adminvm.MiddleName = admin.MiddleName;
            adminvm.LastName = admin.LastName;
            adminvm.MiddleLastName = admin.MiddleLastName;
            adminvm.Email = admin.Email!;
            adminvm.PhoneNumber = admin.PhoneNumber!;
            adminvm.IdentityNumber = admin.IdentityNumber;
            adminvm.BirthDate = admin.BirthDate;
            adminvm.BirthPlace = admin.BirthPlaceName;
            adminvm.City = admin.CityName;
            adminvm.District = admin.DistrictName;
            adminvm.FullAddress = admin.FullAddress;
            adminvm.PictureUri = admin.PictureUri;
            adminvm.HireDate = admin.HireDate;
            adminvm.TerminationDate = admin.TerminationDate;
            adminvm.IsActive = admin.IsActive;
            adminvm.CompanyName = admin.CompanyName;
            adminvm.OccupationName = admin.OccupationName;
            adminvm.DepartmentName = admin.DepartmentName;
            NameMethod(admin);

            return View(adminvm);
        }
        private void NameMethod(ApplicationUser? admin)
        {
            TempData["Name"] = admin.FirstName + " " + (admin.MiddleName != null ? admin.MiddleName : "") + (admin.MiddleLastName != null ? admin.MiddleLastName : "") + " " + admin.LastName;
            TempData["Picture"] = admin.PictureUri;
        }
    }
}
