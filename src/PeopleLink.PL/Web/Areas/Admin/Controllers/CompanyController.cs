using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ICityService _cityService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompanyController(HttpClient httpClient, ICityService cityService, UserManager<ApplicationUser> userManager)
        {
            _httpClient = httpClient;
            _cityService = cityService;
            _userManager = userManager;
        }

        public async Task<IActionResult> AllCompanies()
        {
            var companies = await _httpClient.GetFromJsonAsync<List<CompanyViewModel>>("https://peoplelinkapi1.azurewebsites.net/api/Company");

            var admin = await _userManager.GetUserAsync(User);
            NameMethod(admin);

            return View(companies);
        }

        public async Task<IActionResult> CompanyDetail(int companyId)
        {
            var company = await _httpClient.GetFromJsonAsync<CompanyViewModel>($"https://peoplelinkapi1.azurewebsites.net/api/Company/{companyId}");

            if (company == null)
                return NotFound();

            var admin = await _userManager.GetUserAsync(User);
            NameMethod(admin);
            return View(company);
        }

        public async Task<IActionResult> AddCompany()
        {
            var companyTitles = Enum.GetValues(typeof(CompanyTitle)).Cast<CompanyTitle>().ToList();
            var cities = await _cityService.GetAllAsync();
            var allCities = cities.Data.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            ViewBag.CompanyTitles = companyTitles;
            ViewBag.Cities = allCities;
            var admin = await _userManager.GetUserAsync(User);
            NameMethod(admin);
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCompany(PostCompanyViewModel companyVm)
        {
            var companyTitles = Enum.GetValues(typeof(CompanyTitle)).Cast<CompanyTitle>().ToList();
            var cities = await _cityService.GetAllAsync();
            var allCities = cities.Data.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            ViewBag.CompanyTitles = companyTitles;
            ViewBag.Cities = allCities;

            if (companyVm.MERSISNumber == null || companyVm.TaxNumber == null)
            {
                ModelState.AddModelError("company.MERSISNumber", "Mersis numarası boş olamaz!");
                ModelState.AddModelError("company.TaxNumber", "Vergi numarası numarası boş olamaz!");
                return View();
            }

            if (ModelState.IsValid)
            {
                string cityName = (await _cityService.GetCityAsync(companyVm.CityId)).Name;
                string districtName = (await _cityService.GetCityAsync(companyVm.CityId)).Districts.FirstOrDefault(d => d.Id == companyVm.DistrictId)!.Name;

                companyVm.CityName = cityName;
                companyVm.DistrictName = districtName;

                string ext = Path.GetExtension(companyVm.CompanyLogo.FileName);
                string fileName = Path.Combine(Guid.NewGuid().ToString() + ext);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/document", fileName);

                using (var fileStream = System.IO.File.Create(filePath))
                {
                    companyVm.CompanyLogo.CopyTo(fileStream);
                }

                var newCompany = new AddCompanyViewModel()
                {
                    CompanyName = companyVm.CompanyName,
                    Title = companyVm.Title,
                    MERSISNumber = companyVm.MERSISNumber,
                    TaxNumber = companyVm.TaxNumber,
                    TaxOffice = companyVm.TaxOffice,
                    CompanyLogoUrl = fileName,
                    PhoneNumber = companyVm.PhoneNumber,
                    Address = companyVm.Address,
                    CityId = companyVm.CityId,
                    CityName = companyVm.CityName,
                    DistrictId = companyVm.DistrictId,
                    DistrictName = companyVm.DistrictName,
                    Email = companyVm.Email,
                    EmployeeCount = companyVm.EmployeeCount,
                    EstablishmentYear = companyVm.EstablishmentYear,
                    ContractStartDate = companyVm.ContractStartDate,
                    ContractEndDate = companyVm.ContractEndDate
                };

                await _httpClient.PostAsJsonAsync<AddCompanyViewModel>("https://peoplelinkapi1.azurewebsites.net/api/Company/", newCompany);
                TempData["Success"] = "Şirket başarıyla eklendi.";
                var admin = await _userManager.GetUserAsync(User);
                NameMethod(admin);
                return RedirectToAction("AllCompanies");
            }

            return View();

        }

        public async Task<IActionResult> EditCompany(int companyId)
        {
            var companyTitles = Enum.GetValues(typeof(CompanyTitle)).Cast<CompanyTitle>().ToList();
            var cities = await _cityService.GetAllAsync();
            var allCities = cities.Data.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            ViewBag.CompanyTitles = companyTitles;
            ViewBag.Cities = allCities;

            var updatedCompany = await _httpClient.GetFromJsonAsync<CompanyViewModel>($"https://peoplelinkapi1.azurewebsites.net/api/Company/{companyId}");

            if (updatedCompany == null)
                return NotFound();

            ViewBag.Logo = updatedCompany.CompanyLogoUrl;
            ViewBag.Id = companyId;

            PutCompanyViewModel companyVm = new PutCompanyViewModel()
            {
                Id = updatedCompany.Id,
                CompanyName = updatedCompany.CompanyName,
                Title = updatedCompany.Title,
                MERSISNumber = updatedCompany.MERSISNumber,
                TaxNumber = updatedCompany.TaxNumber,
                TaxOffice = updatedCompany.TaxOffice,
                PhoneNumber = updatedCompany.PhoneNumber,
                Address = updatedCompany.Address,
                CityId = updatedCompany.CityId,
                CityName = updatedCompany.CityName,
                DistrictId = updatedCompany.DistrictId,
                DistrictName = updatedCompany.DistrictName,
                Email = updatedCompany.Email,
                EmployeeCount = updatedCompany.EmployeeCount,
                EstablishmentYear = updatedCompany.EstablishmentYear,
                ContractStartDate = updatedCompany.ContractStartDate,
                ContractEndDate = updatedCompany.ContractEndDate,
                IsActive = updatedCompany.IsActive
            };

            var admin = await _userManager.GetUserAsync(User);
            NameMethod(admin);
            return View(companyVm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCompany(PutCompanyViewModel companyVm)
        {

            var companyTitles = Enum.GetValues(typeof(CompanyTitle)).Cast<CompanyTitle>().ToList();
            var cities = await _cityService.GetAllAsync();
            var allCities = cities.Data.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            ViewBag.CompanyTitles = companyTitles;
            ViewBag.Cities = allCities;

            var company = await _httpClient.GetFromJsonAsync<CompanyViewModel>($"https://peoplelinkapi1.azurewebsites.net/api/Company/{companyVm.Id}");

            //if (companyVm.CompanyLogo != null)
            //{
            //    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/document", company.CompanyLogoUrl);
            //    using (var fileStream = System.IO.File.OpenRead(filePath))
            //    {
            //        companyVm.CompanyLogo.CopyTo(fileStream);
            //    }
            //}

            if (companyVm.MERSISNumber == null || companyVm.TaxNumber == null)
            {
                ModelState.AddModelError("company.MERSISNumber", "Mersis numarası boş olamaz!");
                ModelState.AddModelError("company.TaxNumber", "Vergi numarası numarası boş olamaz!");
                return View();
            }

            if (ModelState.IsValid)
            {
                string cityName = (await _cityService.GetCityAsync(companyVm.CityId)).Name;
                string districtName = (await _cityService.GetCityAsync(companyVm.CityId)).Districts.FirstOrDefault(d => d.Id == companyVm.DistrictId)!.Name;

                companyVm.CityName = cityName;
                companyVm.DistrictName = districtName;

                string ext = Path.GetExtension(companyVm.CompanyLogo.FileName);
                string fileName = Path.Combine(Guid.NewGuid().ToString() + ext);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/document", fileName);

                using (var fileStream = System.IO.File.Create(filePath))
                {
                    companyVm.CompanyLogo.CopyTo(fileStream);
                }

                var updateCompany = new CompanyViewModel()
                {
                    Id = companyVm.Id,
                    CompanyName = companyVm.CompanyName,
                    Title = companyVm.Title,
                    MERSISNumber = companyVm.MERSISNumber,
                    TaxNumber = companyVm.TaxNumber,
                    TaxOffice = companyVm.TaxOffice,
                    CompanyLogoUrl = companyVm.CompanyLogo == null ? company.CompanyLogoUrl : fileName,
                    PhoneNumber = companyVm.PhoneNumber,
                    Address = companyVm.Address,
                    CityId = companyVm.CityId,
                    CityName = companyVm.CityName,
                    DistrictId = companyVm.DistrictId,
                    DistrictName = companyVm.DistrictName,
                    Email = companyVm.Email,
                    EmployeeCount = companyVm.EmployeeCount,
                    EstablishmentYear = companyVm.EstablishmentYear,
                    ContractStartDate = companyVm.ContractStartDate,
                    ContractEndDate = companyVm.ContractEndDate,
                    IsActive = companyVm.IsActive
                };

                await _httpClient.PutAsJsonAsync<CompanyViewModel>("https://peoplelinkapi1.azurewebsites.net/api/Company/" + updateCompany.Id, updateCompany);

                TempData["Success"] = "Şirket başarıyla güncellendi";
                ViewBag.Logo = updateCompany.CompanyLogoUrl;
                var admin = await _userManager.GetUserAsync(User);
                NameMethod(admin);
                return RedirectToAction("AllCompanies");
            }
            else
            {
                //foreach (var error in .Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
            }

            return View(companyVm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            var company = await _httpClient.GetFromJsonAsync<CompanyViewModel>("https://peoplelinkapi1.azurewebsites.net/api/Company/" + companyId);

            await _httpClient.DeleteAsync("https://peoplelinkapi1.azurewebsites.net/api/Company/" + companyId);

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/document", company!.CompanyLogoUrl);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            };

            var admin = await _userManager.GetUserAsync(User);
            NameMethod(admin);
            TempData["Success"] = "Şirket başarıyla silindi.";
            return RedirectToAction("AllCompanies");
        }
        private void NameMethod(ApplicationUser? admin)
        {
            TempData["Name"] = admin!.FirstName + " " + (admin.MiddleName != null ? admin.MiddleName : "") + (admin.MiddleLastName != null ? admin.MiddleLastName : "") + " " + admin.LastName;
            TempData["Picture"] = admin.PictureUri;
        }

        //public async Task<JsonResult> GetDistricts(int cityId, string cityName)
        //{
        //    var city = await _cityService.GetCityAsync(cityId);
        //    cityName = (await _cityService.GetCityAsync(cityId)).Name;

        //    var districts = city.Districts.Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

        //    return new JsonResult(districts);
        //}
    }
}
