using ApplicationCore.Entities;
using ApplicationCore.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? MiddleLastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int BirthPlaceId { get; set; }
        public string BirthPlaceName { get; set; } = null!;
        public string? PictureUri { get; set; }
        public string IdentityNumber { get; set; } = null!;
        public DateTime HireDate { get; set; }
        public DateTime TerminationDate { get; set; }
        public bool IsActive => TerminationDate == DateTime.MinValue ? true : TerminationDate > DateTimeOffset.Now;
        public int OccupationId { get; set; }
        public string OccupationName { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public int CompanyId { get; set; } 
        public string CompanyName { get; set; } = null!;
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int DistrictId { get; set; }
        public string DistrictName { get; set; } = null!;
        public string FullAddress { get; set; } = null!;
        public decimal Salary { get; set; }
        public Gender Gender { get; set; }
        public int AccruedLeave { get; set; }
        public decimal ExpenseAllowance { get; set; }
        public decimal AdvanceAllowance { get; set; }
    }
}
