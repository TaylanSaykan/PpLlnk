using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Attributes;

namespace Web.Areas.Admin.Models
{
    public class PostCompanyViewModel
    {
        [Required(ErrorMessage = "Zorunlu alan")]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "Şirket Adı en az 2, en çok 50 karakter uzunluğunda olmalıdır.", MinimumLength = 2)]
        [CheckIsText]
        public string CompanyName { get; set; } = null!;// Şirket Adı

        [Required(ErrorMessage = "Zorunlu alan")]
        public CompanyTitle Title { get; set; } // Unvan (Limited AŞ, vb.)

        [Required(ErrorMessage = "Zorunlu alan")]
        [Display(Name = "Mersis Numarası")]
        [StringLength(16, ErrorMessage = "{0} {1} karakter uzunluğunda olmalıdır.", MinimumLength = 16)]
        [CheckMersisNumber(ErrorMessage = "MERSIS numarası geçersizdir.")]
        [DataType(DataType.Text, ErrorMessage = "Geçerli veri giriniz.")]
        public string MERSISNumber { get; set; } = null; // MERSIS Numarası 

        [Required(ErrorMessage = "Zorunlu alan")]
        [Display(Name = "Vergi Numarası")]
        [StringLength(11, ErrorMessage = "{0} {1} karakter uzunluğunda olmalıdır.", MinimumLength = 10)]
        [CheckTaxNumber(ErrorMessage = "Vergi numarası geçersizdir.")]
        [DataType(DataType.Text, ErrorMessage = "Geçerli veri giriniz.")]
        public string TaxNumber { get; set; } = null;// Vergi Numarası 

        [Required(ErrorMessage = "Zorunlu alan")]
        [DataType(DataType.Text, ErrorMessage = "Geçerli veri giriniz.")]
        [StringLength(50, ErrorMessage = "Vergi Dairesi en az 2, en çok 50 karakter uzunluğunda olmalıdır.", MinimumLength = 2)]
        [CheckIsText]
        public string TaxOffice { get; set; } = null!;// Vergi Dairesi

        [Required(ErrorMessage = "Zorunlu alan")]
        public IFormFile CompanyLogo { get; set; } // Şirket Logosu (URL olarak saklanabilir)

        [Phone]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [Display(Name = "Telefon Numarası")]
        [RegularExpression(@"^[0][0-9]{10}$", ErrorMessage = "Geçersiz {0}.")]
        [DataType(DataType.Text, ErrorMessage = "Geçerli veri giriniz.")]
        public string PhoneNumber { get; set; } = null!;// Telefon Numarası

        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [Display(Name = "Adres")]
        [StringLength(100, ErrorMessage = "{0} en az {2}, en çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 15)]
        [DataType(DataType.Text, ErrorMessage = "Geçerli veri giriniz.")]
        public string Address { get; set; } = null!;// Adres

        [Required(ErrorMessage = "Zorunlu alan")]
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;

        [Required(ErrorMessage = "Zorunlu alan")]
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = null!;

        [Required(ErrorMessage = "Zorunlu alan")]
        [Display(Name = "Email")]
        [StringLength(100, ErrorMessage = "{0} en az {2}, en çok {1} karakter uzunluğunda olmalıdır.", MinimumLength = 11)]
        [DataType(DataType.Text, ErrorMessage = "Geçerli veri giriniz.")]
        public string Email { get; set; } = null!;// E-posta Adresi

        [Required(ErrorMessage = "Zorunlu alan")]
        public int EmployeeCount { get; set; } // Çalışan Sayısı

        [Required(ErrorMessage = "Zorunlu alan")]
        public int EstablishmentYear { get; set; } // Kuruluş Yılı

        [Required(ErrorMessage = "Zorunlu alan")]
        [DataType(DataType.Date, ErrorMessage = "Sayi alanı boş bırakılamaz.")]
        public DateTime ContractStartDate { get; set; } // Sözleşme Başlangıç Tarihi

        [Required(ErrorMessage = "Zorunlu alan")]
        [DataType(DataType.Date, ErrorMessage = "Sayi alanı boş bırakılamaz.")]
        public DateTime ContractEndDate { get; set; } // Sözleşme Bitiş Tarihi

        public bool IsActive { get; set; } // Aktiflik Durumu (true ise aktif, false ise pasif)

    }
}
