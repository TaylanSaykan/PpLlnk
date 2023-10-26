// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ApplicationCore.Entities.CityAndDistrictEntities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Employee.Extensions;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICityService _cityService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICityService cityService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cityService = cityService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public List<SelectListItem> Cities { get; set; } = new();

        public int DistrictId { get; set; }

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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>

            [Display(Name = "Profil Fotoğrafı")]
            [DataType(DataType.Upload)]
            [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Sadece .jpg, .jpeg, .png formatlarında fotoğraf yüklenebilir.")]
            public IFormFile ProfileImage { get; set; }

            [Display(Name = "İl")]
            public int CityId { get; set; }

            public string CityName { get; set; }

            [Display(Name = "İlçe")]
            public int DistrictId { get; set; }

            public string DistrictName { get; set; }

            [Display(Name = "Adres")]
            [StringLength(100, ErrorMessage = "{0} en az {2}, en çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 15)]
            public string FullAddress { get; set; } = null!;

            [Phone]
            [Display(Name = "Telefon Numarası")]
            [RegularExpression(@"^[5][0-9]{9}$", ErrorMessage = "Geçersiz {0}.")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var cities = await _cityService.GetAllAsync();

            Username = userName;
            Cities = cities.Data.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            DistrictId = user.DistrictId;

            Input = new InputModel
            {
                CityId = user.CityId,
                CityName = user.CityName,
                DistrictId = user.DistrictId,
                DistrictName = user.DistrictName,
                FullAddress = user.FullAddress,
                PhoneNumber = phoneNumber
            };

            //string ext = Path.GetExtension(user.PictureUri).Substring(1);
            //string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", user.PictureUri);

            //if (System.IO.File.Exists(imagePath))
            //{
            //    FileStream stream = System.IO.File.OpenRead(imagePath);

            //    var formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(imagePath))
            //    {
            //        Headers = new HeaderDictionary(),
            //        ContentType = $"image/{ext}"
            //    };

            //    Input.ProfileImage = formFile;
            //}
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            TempData["Name"] = user.FirstName + " " + (user.MiddleName != null ? user.MiddleName : "") + (user.MiddleLastName != null ? user.MiddleLastName : "") + " " + user.LastName;
            TempData["Picture"] = user.PictureUri;

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.CityId != user.CityId)
            {
                user.CityId = Input.CityId;
                user.CityName = (await _cityService.GetCityAsync(Input.CityId)).Name;
            }

            if (Input.DistrictId != user.DistrictId)
            {
                user.DistrictId = Input.DistrictId;
                user.DistrictName = (await _cityService.GetCityAsync(Input.CityId)).Districts.FirstOrDefault(d => d.Id == Input.DistrictId).Name;
            }

            if (Input.FullAddress != user.FullAddress)
                user.FullAddress = Input.FullAddress;


            if (Input.ProfileImage != null)
            {
                if (ModelState.IsValid)
                {
                    string ext = Path.GetExtension(Input.ProfileImage.FileName);
                    string fileName = Path.Combine(Guid.NewGuid().ToString() + ext);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                    using (var fileStream = System.IO.File.Create(imagePath))
                    {
                        Input.ProfileImage.CopyTo(fileStream);
                    }

                    user.PictureUri = fileName;
                }
            }


            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Profil bilgileri başarıyla güncellendi.";
            return RedirectToPage();
        }
    }
}
