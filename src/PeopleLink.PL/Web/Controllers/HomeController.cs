using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICityService _cityService;

        public HomeController(ILogger<HomeController> logger, ICityService cityService)
        {
            _logger = logger;
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<JsonResult> GetDistricts(int cityId)
        {
            var city = await _cityService.GetCityAsync(cityId);

            var districts = city.Districts.Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

            return new JsonResult(districts);
        }
    }
}