// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Entities.CityAndDistrictEntities;
using ApplicationCore.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Web.Areas.Employee.Extensions;
using Web.Areas.Manager;
using Web.Attributes;

namespace Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IRepository<Occupation> _occupationRepo;
        private readonly IRepository<Department> _departmentRepo;
        private readonly ICityService _cityService;
        private readonly IMailService _mailService;
        private readonly ICompanyService _companyService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IRepository<Occupation> occupationRepo,
            IRepository<Department> departmentRepo,
            ICityService cityService,
            IMailService mailService,
            ICompanyService companyService
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _occupationRepo = occupationRepo;
            _departmentRepo = departmentRepo;
            _cityService = cityService;
            _mailService = mailService;
            _companyService = companyService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public List<SelectListItem> Occupations { get; set; } = new();

        public List<SelectListItem> Departments { get; set; } = new();

        public List<SelectListItem> Cities { get; set; } = new();

        public List<SelectListItem> Companies { get; set; } = new();

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Ad")]
            [DataType(DataType.Text)]
            [StringLength(50, ErrorMessage = "{0} bölümü en az {2}, en çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 2)]
            [CheckIsText]
            public string FirstName { get; set; }

            [Display(Name = "İkinci Ad")]
            [DataType(DataType.Text)]
            [StringLength(50, ErrorMessage = "{0} bölümü en az {2}, en çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 3)]
            [CheckIsText]
            public string MiddleName { get; set; }

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Soyad")]
            [DataType(DataType.Text)]
            [StringLength(50, ErrorMessage = "{0} bölümü en az {2}, en çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 3)]
            [CheckIsText]
            public string LastName { get; set; }

            [Display(Name = "İkinci Soyad")]
            [DataType(DataType.Text)]
            [StringLength(50, ErrorMessage = "{0} bölümü en az {2}, en çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 3)]
            [CheckIsText]
            public string MiddleLastName { get; set; }

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Profil Fotoğrafı")]
            [DataType(DataType.Upload)]
            [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Sadece .jpg, .jpeg, .png formatlarında fotoğraf yüklenebilir.")]
            public IFormFile ProfileImage { get; set; }

            [Phone]
            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Telefon Numarası")]
            [RegularExpression(@"^[5][0-9]{9}$", ErrorMessage = "Geçersiz {0}.")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Doğum Tarihi")]
            [DataType(DataType.Date)]
            [CheckBirthDate]
            public DateTime BirthDate { get; set; }

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Doğum Yeri")]
            public int BirthPlaceId { get; set; }

            public string BirthPlaceName { get; set; } = null!;

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Kimlik Numarası")]
            [StringLength(11, ErrorMessage = "{0} {1} karakter uzunluğunda olmalıdır.", MinimumLength = 11)]
            [CheckIdentityNumber]
            public string IdentityNumber { get; set; }

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "İşe Giriş Tarihi")]
            [DataType(DataType.Date)]
            [CheckHireDate]
            public DateTime HireDate { get; set; }

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Meslek")]
            public int OccupationId { get; set; }

            public string OccupationName { get; set; }

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Departman")]
            public int DepartmentId { get; set; }

            public string DepartmentName { get; set; }

            //[Required(ErrorMessage = "{0} alanı zorunludur.")]
            //[Display(Name = "Şirket Adı")]
            //public int CompanyId { get; set; }
            //public string CompanyName { get; set; } = null!;

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "İl")]
            public int CityId { get; set; }

            public string CityName { get; set; }

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "İlçe")]
            public int DistrictId { get; set; }

            public string DistrictName { get; set; }

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Adres")]
            [StringLength(100, ErrorMessage = "{0} en az {2}, en çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 15)]
            public string FullAddress { get; set; } = null!;

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [DataType(DataType.Currency)]
            [Display(Name = "Maaş")]
            [Range(11402, 150000, ErrorMessage = "{0} en az {1} TL, en çok {2} TL tutarında olabilir.")]
            public decimal Salary { get; set; }

            [Required(ErrorMessage = "{0} alanı zorunludur.")]
            [Display(Name = "Cinsiyet")]
            public Gender Gender { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            await GetData();

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        private async Task GetData()
        {
            var occupations = await _occupationRepo.GetAllAsync();
            var departments = await _departmentRepo.GetAllAsync();
            var cities = await _cityService.GetAllAsync();
            var manager = await _userManager.GetUserAsync(User);
            var companies = await _companyService.GetCompaniesAsync();

            Occupations = occupations.Select(o => new SelectListItem(o.Name, o.Id.ToString())).ToList();
            Departments = departments.Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();
            Cities = cities.Data.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            Companies = companies.Select(c => new SelectListItem(c.CompanyName, c.Id.ToString())).ToList();
            TempData["Picture"] = manager.PictureUri;
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            await GetData();

            var admin = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(admin, "Admin"))
            {
                Input.OccupationId = 8;
                Input.DepartmentId = 8;
                Input.OccupationName = "Şirket Yöneticisi";
                Input.DepartmentName = "Yönetim ve İdari İşler";
            }

            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

                if (_userManager.Users.Any(x => x.IdentityNumber == Input.IdentityNumber))
                {
                    TempData["WarningMessage"] = "Üzgünüz, bu kimlik numarası zaten başka bir kullanıcı tarafından kullanılmaktadır.";

                    return Page();
                };


                var loginUser = await _userManager.GetUserAsync(User);

                var user = CreateUser();

                if (await _userManager.IsInRoleAsync(admin, "Admin"))
                {
                    user.Email = $"{ReplaceTurkishCharacters(Input.FirstName).ToLower()}.{ReplaceTurkishCharacters(Input.LastName).ToLower()}@bilgeadam.com";
                }
                else
                {
                    user.Email = $"{ReplaceTurkishCharacters(Input.FirstName).ToLower()}.{ReplaceTurkishCharacters(Input.LastName).ToLower()}@bilgeadamboost.com";
                }

                //user.Email = $"{ReplaceTurkishCharacters(Input.FirstName).ToLower()}.{ReplaceTurkishCharacters(Input.LastName).ToLower()}@bilgeadamboost.com";
                user.FirstName = ti.ToTitleCase(Input.FirstName);
                user.LastName = ti.ToTitleCase(Input.LastName);
                user.MiddleName = Input.MiddleName == null ? "" : ti.ToTitleCase(Input.MiddleName);
                user.MiddleLastName = Input.MiddleLastName == null ? "" : ti.ToTitleCase(Input.MiddleLastName);
                user.PhoneNumber = Input.PhoneNumber;
                user.BirthDate = Input.BirthDate;
                user.BirthPlaceId = Input.BirthPlaceId;
                user.BirthPlaceName = (await _cityService.GetCityAsync(Input.BirthPlaceId)).Name;
                user.IdentityNumber = Input.IdentityNumber;
                user.HireDate = Input.HireDate;
                user.OccupationId = Input.OccupationId;
                user.OccupationName = (await _occupationRepo.GetByIdAsync(Input.OccupationId)).Name;
                user.DepartmentId = Input.DepartmentId;
                user.DepartmentName = (await _departmentRepo.GetByIdAsync(Input.DepartmentId)).Name;
                user.CityId = Input.CityId;
                user.CityName = (await _cityService.GetCityAsync(Input.CityId)).Name;
                user.DistrictId = Input.DistrictId;
                user.DistrictName = (await _cityService.GetCityAsync(Input.CityId)).Districts.FirstOrDefault(d => d.Id == Input.DistrictId).Name;
                if (await _userManager.IsInRoleAsync(admin, "Admin"))
                {
                    user.CompanyId = 0;
                    user.CompanyName = string.Empty;
                }
                else
                {
                    user.CompanyId = loginUser.CompanyId;
                    user.CompanyName = loginUser.CompanyName;
                }
                user.FullAddress = Input.FullAddress;
                user.Salary = Input.Salary;
                user.Gender = Input.Gender;
                user.AccruedLeave = user.IsActive ? (DateTimeOffset.Now > user.HireDate.AddYears(1) ? (DateTimeOffset.Now < user.HireDate.AddYears(6) ? 14 : 20) : 0) : 0;
                user.ExpenseAllowance = user.Salary * 3;
                user.AdvanceAllowance = user.Salary * 3;

                string ext = Path.GetExtension(Input.ProfileImage.FileName);
                string fileName = Path.Combine(Guid.NewGuid().ToString() + ext);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                using (var fileStream = System.IO.File.Create(imagePath))
                {
                    Input.ProfileImage.CopyTo(fileStream);
                }

                user.PictureUri = fileName;

                string password = AuthorizationConstant.PasswordCreator.CreateDefaultPassword();

                if (user.OccupationId == 8)
                {
                    await _userManager.AddToRoleAsync(user, AuthorizationConstant.Roles.MANAGER);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, AuthorizationConstant.Roles.EMPLOYEE);
                }

                await _userStore.SetUserNameAsync(user, user.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, user.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _mailService.SendEmailAsync(user.Email, password, user.Id);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {

                        if (await _userManager.IsInRoleAsync(admin, "Admin"))
                        {
                            return RedirectToAction("AllManagers", "Manager", new { area = "Admin" });
                        }
                        else if (await _userManager.IsInRoleAsync(admin, "Manager"))
                        {
                            return RedirectToAction("AllEmployees", "Employee", new { area = "Manager" });

                        }

                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                await _mailService.SendEmailAsync(user.Email, password, user.Id);
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        public static string ReplaceTurkishCharacters(string turkishWord)
        {
            string source = "ığüşöçĞÜŞİÖÇ";
            string destination = "igusocGUSIOC";

            string result = turkishWord;

            for (int i = 0; i < source.Length; i++)
            {
                result = result.Replace(source[i], destination[i]);
            }

            return result;
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}