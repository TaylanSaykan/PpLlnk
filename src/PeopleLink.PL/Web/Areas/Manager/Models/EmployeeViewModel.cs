namespace Web.Areas.Manager.Models
{
    public class EmployeeViewModel
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
        public string? Picture { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; } = null!;
        public int OccupationId { get; set; }
        public int DepartmentId { get; set; }
        public string OccupationName { get; set; }
        public string DepartmentName { get; set; }
    }
}
