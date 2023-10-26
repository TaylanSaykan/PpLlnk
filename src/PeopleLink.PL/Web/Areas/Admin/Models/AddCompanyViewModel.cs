namespace Web.Areas.Admin.Models
{
    public class AddCompanyViewModel
    {
        public string CompanyName { get; set; } // Şirket Adı
        public CompanyTitle Title { get; set; } // Unvan (Limited AŞ, vb.)
        public string MERSISNumber { get; set; } // MERSIS Numarası 
        public string TaxNumber { get; set; } // Vergi Numarası 
        public string TaxOffice { get; set; } // Vergi Dairesi
        public string CompanyLogoUrl { get; set; } // Şirket Logosu (URL olarak saklanabilir)
        public string PhoneNumber { get; set; } // Telefon Numarası
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = null!;
        public string Address { get; set; } // Adres
        public string Email { get; set; } // E-posta Adresi
        public int EmployeeCount { get; set; } // Çalışan Sayısı
        public int EstablishmentYear { get; set; } // Kuruluş Yılı
        public DateTime ContractStartDate { get; set; } // Sözleşme Başlangıç Tarihi
        public DateTime ContractEndDate { get; set; } // Sözleşme Bitiş Tarihi
        
    }
}
