namespace Web.Areas.Admin.Models
{
    public class AdminDetailsViewModel
    {
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? MiddleLastName { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string IdentityNumber { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; } = null!;
        public string City { get; set; } = null!;
        public string District { get; set; } = null!;
        public string FullAddress { get; set; } = null!;
        public string? PictureUri { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; } = null!;
        public string OccupationName { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
    }
}
