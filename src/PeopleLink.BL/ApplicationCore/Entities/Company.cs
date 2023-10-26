using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; } = null!;// Şirket Adı
        public CompanyTitle Title { get; set; } // Unvan (Limited AŞ, vb.)
        public string MERSISNumber { get; set; } = null!;// MERSIS Numarası 
        public string TaxNumber { get; set; } = null!;// Vergi Numarası 
        public string TaxOffice { get; set; } = null!;// Vergi Dairesi
        public string CompanyLogoUrl { get; set; } = null!;// Şirket Logosu (URL olarak saklanabilir)
        public string PhoneNumber { get; set; } = null!; // Telefon Numarası
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = null!;
        public string Address { get; set; } = null!;// Adres
        public string Email { get; set; } = null!; // E-posta Adresi
        public int EmployeeCount { get; set; } // Çalışan Sayısı
        public int EstablishmentYear { get; set; } // Kuruluş Yılı
        public DateTime ContractStartDate { get; set; } // Sözleşme Başlangıç Tarihi
        public DateTime ContractEndDate { get; set; } // Sözleşme Bitiş Tarihi
        public bool IsActive { get; set; } // Aktiflik Durumu (true ise aktif, false ise pasif)
    }
}

