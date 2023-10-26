using System.ComponentModel.DataAnnotations;
using Web.Areas.Employee.Extensions;

namespace Web.Areas.Employee.Models
{
    public class PostExpenseViewModel
    {
        public string EmployeeId { get; set; } = null!;

        [Required(ErrorMessage = "Tutar alanı zorunludur.")]
        [Range(10, 10000, ErrorMessage = "Geçerli bir pozitif sayı girin.")]
        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "Lütfen geçerli bir değer giriniz.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Lütfen para birimi seçiniz.")]
        public Currency Currency { get; set; }

        [Required(ErrorMessage = "Lütfen harcama türü seçiniz.")]
        public ExpenseType ExpenseType { get; set; }

        [Required(ErrorMessage = "Lütfen harcama dökümanı yükleyiniz.")]
        [AllowedExtensions(new string[] { ".pdf", ".jpg", ".jpeg", ".png" }, ErrorMessage = "Sadece .pdf, .jpg, .jpeg, .png formatlarında belge girişi yapılabilir.")]
        public IFormFile Document { get; set; } = null!;

        public string Status { get; set; } = null!;

    }
}
