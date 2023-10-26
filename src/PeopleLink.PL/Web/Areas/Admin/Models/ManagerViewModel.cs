namespace Web.Areas.Admin.Models
{
    public class ManagerViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MiddleLastName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string FullAddress { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
        public string OccupationName { get; set; }
        public string DepartmentName { get; set; }
    }
}