using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Employee.Models
{
    public class AdvanceViewModel
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; } = null!;

        [Required(ErrorMessage = "Lütfen avans türünü seçiniz.")]
        public AdvanceType AdvanceType { get; set; }
        [Required(ErrorMessage = "Lütfen birim tutar giriniz.")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Lütfen para birimi giriniz.")]
        public Currency Currency { get; set; }
        public string Status { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        [Required(ErrorMessage = "Lütfen açıklama giriniz.")]
        [StringLength(50, ErrorMessage = "Açıklama {2}-{1} arasında olmalıdır.", MinimumLength = 20)]
        public string Description { get; set; } = null!;
        public DateTime? ApprovalDate { get; set; }
    }
}
