using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.LeaveSpecifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Employee.Models;

namespace Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class LeaveController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;

        public LeaveController(HttpClient httpClient, UserManager<ApplicationUser> userManager)
        {
            _httpClient = httpClient;
            _userManager = userManager;
        }

        public async Task<IActionResult> LeaveRequest()  // İzin talep sayfasına gider
        {
            var user = await _userManager.GetUserAsync(User);
            var leaveTypes = Enum.GetValues(typeof(LeaveType)).Cast<LeaveType>().ToList();

            if (user!.Gender == Gender.Male)
                leaveTypes = leaveTypes.Where(lt => lt != LeaveType.MaternityLeave).ToList();
            else if (user.Gender == Gender.Female)
                leaveTypes = leaveTypes.Where(lt => lt != LeaveType.PaternityLeave).ToList();

            ViewData["UserId"] = user!.Id;
            ViewBag.LeaveTypes = leaveTypes;
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            ViewData["AccruedLeave"] = user.AccruedLeave;
            NameMethod(user);
            return View();

        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> LeaveRequest(LeaveViewModel leaveViewModel)  // Formdan gelen izin verilerini apiye gönderip kaydettirir.
        {
            var user = await _userManager.GetUserAsync(User);
            string apiUrl = $"https://peoplelinkapi1.azurewebsites.net/api/Leave?employeeId={user.Id}";

            var leaves = await _httpClient.GetFromJsonAsync<List<LeaveViewModel>>(apiUrl);
            var leaveTypes = Enum.GetValues(typeof(LeaveType)).Cast<LeaveType>().ToList();

            ViewBag.LeaveTypes = leaveTypes;

            if (ModelState.IsValid)
            {
                if (user!.Gender == Gender.Male)
                    leaveTypes = leaveTypes.Where(lt => lt != LeaveType.MaternityLeave).ToList();
                else if (user.Gender == Gender.Female)
                    leaveTypes = leaveTypes.Where(lt => lt != LeaveType.PaternityLeave).ToList();  

                if (leaves!.Any(l => l.Status == leaveViewModel.Status && l.LeaveType == leaveViewModel.LeaveType))
                {
                    TempData["HasPendingLeave"] = "Onay bekleyen izin talebiniz bulunmaktadır.";
                    return RedirectToAction("AllLeaves");
                }
                else
                {

                    if (leaveViewModel.LeaveType == LeaveType.AnnualLeave)
                    {
                        if (user.AccruedLeave == 0)
                        {
                            TempData["AccuredLeave"] = "Yıllık izin talep edebileceğiniz gün bitmiştir.";
                            ViewData["AccruedLeave"] = user.AccruedLeave;
                            return RedirectToAction("AllLeaves");
                        }
                        else if (leaveViewModel.TotalDays > user.AccruedLeave)
                        {
                            TempData["AccuredLeave"] = "Talep edilen gün sayısı izin hakkınızdan fazladır.";
                            ViewData["AccruedLeave"] = user.AccruedLeave;
                            return RedirectToAction("AllLeaves");
                        }
                        else
                        {
                            user.AccruedLeave = user.AccruedLeave - leaveViewModel.TotalDays;
                        }

                    }
                    var response = await _httpClient.PostAsJsonAsync("https://peoplelinkapi1.azurewebsites.net/api/Leave", leaveViewModel);

                    await _userManager.UpdateAsync(user);
                    TempData["SuccessLeave"] = "İzin talebi başarıyla oluşturuldu.";
                    ViewData["AccruedLeave"] = user.AccruedLeave;
                    NameMethod(user);
                    return RedirectToAction("AllLeaves");
                }
            }

            ViewData["AccruedLeave"] = user.AccruedLeave;
            return View();
        }

        public async Task<IActionResult> AllLeaves(string? status)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = _userManager.GetUserId(User);
            string apiUrl = $"https://peoplelinkapi1.azurewebsites.net/api/Leave?employeeId={userId}";
            string apiUrlWithStatus = $"https://peoplelinkapi1.azurewebsites.net/api/Leave?approvalStatus={status}&employeeId={userId}";
            TempData["Picture"] = (await _userManager.GetUserAsync(User))!.PictureUri;
            NameMethod(user);
            ViewBag.Status = status;

            if (status != null)
            {
                var leaveWithStatus = await _httpClient.GetFromJsonAsync<List<LeaveViewModel>>(apiUrlWithStatus);
                return View(leaveWithStatus);
            }

            var leaves = await _httpClient.GetFromJsonAsync<List<LeaveViewModel>>(apiUrl);
            return View(leaves);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLeave(int leaveId)
        {
            var leave = await _httpClient.GetFromJsonAsync<LeaveViewModel>($"https://peoplelinkapi1.azurewebsites.net/api/Leave/{leaveId}");
            var user = await _userManager.GetUserAsync(User);

            user.AccruedLeave = user.AccruedLeave + leave.TotalDays;
            await _userManager.UpdateAsync(user);
            NameMethod(user);
            await _httpClient.DeleteAsync($"https://peoplelinkapi1.azurewebsites.net/api/Leave/{leaveId}");

            TempData["DeleteSuccessLeave"] = "İzin talebi başarıyla silindi.";
            return RedirectToAction("AllLeaves");
        }
        private void NameMethod(ApplicationUser? user)
        {
            TempData["Name"] = user.FirstName + " " + (user.MiddleName != null ? user.MiddleName : "") + (user.MiddleLastName != null ? user.MiddleLastName : "") + " " + user.LastName;
            TempData["Picture"] = user.PictureUri;
        }

    }
}
