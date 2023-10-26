using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Employee.Models;
using Microsoft.AspNetCore.Authorization;
using Web.Areas.Identity.Pages.Account;



namespace Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class AdvanceController : Controller
    {
        private readonly HttpClient _httpClient;
        private UserManager<ApplicationUser> _userManager;

        public AdvanceController(HttpClient httpClient, UserManager<ApplicationUser> userManager)
        {
            _httpClient = httpClient;
            _userManager = userManager;

        }

        public async Task<IActionResult> AdvanceRequest()
        {
            var user = await _userManager.GetUserAsync(User);
            var advancetypes = Enum.GetValues(typeof(AdvanceType)).Cast<AdvanceType>().ToList();
            var currencyTypes = Enum.GetValues(typeof(Currency)).Cast<Currency>().ToList();

            ViewData["UserId"] = user.Id;
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            ViewData["Amount"] = user.AdvanceAllowance;
            ViewBag.AdvanceType = advancetypes;
            ViewBag.CurrentType = currencyTypes;
            NameMethod(user);
            return View();

            
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> AdvanceRequest(AdvanceViewModel advanceViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["Amount"] = user.AdvanceAllowance;
            NameMethod(user);

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                string apiUrl = $"https://peoplelinkapi1.azurewebsites.net/api/Advance?employeeId={userId}";

                var advances = await _httpClient.GetFromJsonAsync<List<AdvanceViewModel>>(apiUrl);

                var advanceId = advances!.Where(x => x.Status == advanceViewModel.Status && x.AdvanceType == advanceViewModel.AdvanceType).Select(x => x.Id).FirstOrDefault();

                if (advanceId != 0)
                {
                    TempData["HasPendingAdvance"] = "Onay bekleyen avans talebiniz bulunmaktadır.";
                    return RedirectToAction("AllAdvances");
                }
                else
                {
                    var advanceAmount = advanceViewModel.Currency == Currency.TL ? advanceViewModel.Amount
               : advanceViewModel.Currency == Currency.Dolar ? advanceViewModel.Amount * 27
               : (advanceViewModel.Currency == Currency.Euro ? advanceViewModel.Amount * 29 : advanceViewModel.Amount * 33);

                    if (advanceViewModel.AdvanceType == AdvanceType.Individual)
                    {
                        if (user!.AdvanceAllowance < advanceAmount)
                        {
                            TempData["HasPendingAdvance"] = $"Talep edebileceğiniz miktarı aştınız. Talep edebileceğiniz güncel tutar:{user.AdvanceAllowance}";
                            return RedirectToAction("AllAdvances");
                        }

                    }
                    else if (advanceViewModel.AdvanceType == AdvanceType.Corporate)
                    {
                        if (advanceViewModel.Currency == Currency.TL && advanceViewModel.Amount > 1000000)
                        {
                            TempData["HasPendingAdvance"] = $"1.000.000 {advanceViewModel.Currency} üzeri avans talepleri için lütfen yöneticinize başvurunuz.";
                            return RedirectToAction("AllAdvances");
                        }
                        else if (advanceViewModel.Currency != Currency.TL && advanceViewModel.Amount > 35000)
                        {
                            TempData["HasPendingAdvance"] = $"35.000 {advanceViewModel.Currency} üzeri avans talepleri için lütfen yöneticinize başvurunuz.";
                            return RedirectToAction("AllAdvances");
                        }
                    }

                    var resp = await _httpClient.PostAsJsonAsync("https://peoplelinkapi1.azurewebsites.net/api/Advance", advanceViewModel);

                    user.AdvanceAllowance = user.AdvanceAllowance - advanceAmount;
                    await _userManager.UpdateAsync(user);
                    ViewData["Amount"] = user.AdvanceAllowance;

                    TempData["SuccessAdvance"] = "Avans talebi başarıyla oluşturuldu.";
                    return RedirectToAction("AllAdvances");
                }

            }
            
            return View();
        }


        public async Task<IActionResult> AllAdvances(string? status)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = _userManager.GetUserId(User);
            string apiUrl = $"https://peoplelinkapi1.azurewebsites.net/api/Advance?employeeId={userId}";
            string apiUrlWithStatus = $"https://peoplelinkapi1.azurewebsites.net/api/Advance?status={status}&employeeId={userId}";
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            ViewBag.Status = status;
            ViewBag.HasPendingAdvance = TempData["HasPendingAdvance"];
            ViewBag.SuccessAdvance = TempData["SuccessAdvance"];
            ViewBag.DeleteSuccessAdvance = TempData["DeleteSuccessAdvance"];
            NameMethod(user);

            if (status != null)
            {
                var advanceWithStatus = await _httpClient.GetFromJsonAsync<List<AdvanceViewModel>>(apiUrlWithStatus);

                if (advanceWithStatus != null)

                    return View(advanceWithStatus);
            }
            var advances = await _httpClient.GetFromJsonAsync<List<AdvanceViewModel>>(apiUrl);
            return View(advances);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdvance(int advanceId)
        {
            var advance = await _httpClient.GetFromJsonAsync<AdvanceViewModel>($"https://peoplelinkapi1.azurewebsites.net/api/Advance/{advanceId}");
            var user = await _userManager.GetUserAsync(User);
            NameMethod(user);

            var advanceAmount = advance.Currency == Currency.TL ? advance.Amount
               : advance.Currency == Currency.Dolar ? advance.Amount * 27
               : (advance.Currency == Currency.Euro ? advance.Amount * 29 : advance.Amount * 33);

            user.AdvanceAllowance = user.AdvanceAllowance + advanceAmount;
            await _userManager.UpdateAsync(user);

            await _httpClient.DeleteAsync($"https://peoplelinkapi1.azurewebsites.net/api/Advance/{advanceId}");
            TempData["DeleteSuccessAdvance"] = "Avans talebi başarıyla silindi.";
            return RedirectToAction("AllAdvances");
        }

        private void NameMethod(ApplicationUser? user)
        {
            TempData["Name"] = user.FirstName + " " + (user.MiddleName != null ? user.MiddleName : "") + (user.MiddleLastName != null ? user.MiddleLastName : "") + " " + user.LastName;
            TempData["Picture"] = user.PictureUri;
        }
    }
}
