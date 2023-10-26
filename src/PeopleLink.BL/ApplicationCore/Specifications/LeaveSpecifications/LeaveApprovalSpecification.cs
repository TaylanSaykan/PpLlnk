using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications.LeaveSpecifications
{
    public class LeaveApprovalSpecification : Specification<Leave>
    {
        // AllLeaves sayfasında izin türlerine göre filtreleme
        // EmployeeIdye göre kişinin tüm izinleri ve o izinler arasında 3 oany arasında filtreleme
        public LeaveApprovalSpecification(string? approvalStatus, string employeeId)
        {
            if (approvalStatus != null)
            {
                Query.Where(x => x.EmployeeId == employeeId).Where(x => x.Status == approvalStatus);
            }

            Query.Where(x => x.EmployeeId == employeeId);
        }
    }
}
