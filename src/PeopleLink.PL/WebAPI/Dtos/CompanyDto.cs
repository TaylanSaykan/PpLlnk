using ApplicationCore.Enums;

namespace WebAPI.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }

        public string CompanyName { get; set; } = null!;

        public CompanyTitle Title { get; set; }

        public string MERSISNumber { get; set; } = null!;

        public string TaxNumber { get; set; } = null!;

        public string TaxOffice { get; set; } = null!;

        public string CompanyLogoUrl { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public int CityId { get; set; }

        public string CityName { get; set; } = null!;

        public int DistrictId { get; set; }

        public string DistrictName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int EmployeeCount { get; set; }

        public int EstablishmentYear { get; set; }

        public DateTime ContractStartDate { get; set; }

        public DateTime ContractEndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
