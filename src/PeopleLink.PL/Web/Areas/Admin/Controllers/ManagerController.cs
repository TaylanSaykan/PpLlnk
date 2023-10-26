using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.Areas.Admin.Models;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.ComponentModel.Design;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HttpClient _httpClient;

        public ManagerController(UserManager<ApplicationUser> userManager, HttpClient httpClient)
        {
            _userManager = userManager;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> AllManagers()
        {
            var manager = await _userManager.GetUserAsync(User);
            var managers = await _userManager.GetUsersInRoleAsync("Manager");
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            TempData["Name"] = manager.FirstName + " " + (manager.MiddleName != null ? manager.MiddleName : "") + (manager.MiddleLastName != null ? manager.MiddleLastName : "") + " " + manager.LastName;

            return View(managers);
        }

        public async Task<IActionResult> DetailManager(string managerId)
        {
            var employee = await _userManager.FindByIdAsync(managerId);
            if (employee == null)
            {
                return NotFound();
            }
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;

            return View(employee);
        }

        public async Task<IActionResult> AssignManagerToCompany()
        {
            var manager = await _userManager.GetUserAsync(User);
            var managers = await _userManager.GetUsersInRoleAsync("Manager");
            var companies = await _httpClient.GetFromJsonAsync<List<CompanyViewModel>>("https://peoplelinkapi1.azurewebsites.net/api/Company");
            var companiesList = companies.Select(c => new SelectListItem(c.CompanyName, c.Id.ToString())).ToList();
            var managersList = managers.Where(x => x.CompanyId == 0).Select(c => new SelectListItem($"{c.FirstName} {c.LastName}", c.Id.ToString())).ToList();

            ViewBag.Companies = companiesList;
            ViewBag.Managers = managersList;
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            TempData["Name"] = manager.FirstName + " " + (manager.MiddleName != null ? manager.MiddleName : "") + (manager.MiddleLastName != null ? manager.MiddleLastName : "") + " " + manager.LastName;


            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignManagerToCompany(AssignViewModel vm)
        {
            var manager = await _userManager.FindByIdAsync(vm.ManagerId);
            var company = await _httpClient.GetFromJsonAsync<CompanyViewModel>($"https://peoplelinkapi1.azurewebsites.net/api/Company/{vm.CompanyId}");

            if (ModelState.IsValid)
            {
                manager.CompanyId = company.Id;
                manager.CompanyName = company.CompanyName;
                TempData["Name"] = manager.FirstName + " " + (manager.MiddleName != null ? manager.MiddleName : "") + (manager.MiddleLastName != null ? manager.MiddleLastName : "") + " " + manager.LastName;

                await _userManager.UpdateAsync(manager);

                TempData["Success"] = "Başarıyla atama yapıldı.";
                return RedirectToAction("AllManagers");
            }

            return View();
        }
    }
}
