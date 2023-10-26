using ApplicationCore.Enums;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Employee.Models
{
    public class LeaveViewModel
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; } = null!;

        [Required(ErrorMessage = "Lütfen başlangıç tarihi seçiniz.")]
        [DisplayFormat(NullDisplayText = "Lütfen bir tarih seçiniz.")]
        public DateTime StartingDate { get; set; }

        [Required(ErrorMessage = "Lütfen bitiş tarihi seçiniz.")]
        [DisplayFormat(NullDisplayText = "Lütfen bir tarih seçiniz.")]
        public DateTime EndingDate { get; set; }

        [Required(ErrorMessage = "Lütfen izin türünü seçiniz.")]
        public LeaveType LeaveType { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [Required(ErrorMessage = "Toplam gün sayısı zorunludur.")]
        [DisplayFormat(NullDisplayText = "Gün sayısı zorunludur.")]
        public int TotalDays { get; set; }

        public string Status { get; set; } = null!;
    }
}
