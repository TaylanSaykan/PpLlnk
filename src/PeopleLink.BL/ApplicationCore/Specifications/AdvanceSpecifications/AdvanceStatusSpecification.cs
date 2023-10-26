using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications.AdvanceSpecifications
{
    public class AdvanceStatusSpecification : Specification<Advance>
    {
        public AdvanceStatusSpecification( string? status, string employeeId)
        {
            if (status != null)
            {
                Query.Where(x => x.EmployeeId == employeeId).Where(x => x.Status == status);
            }

            Query.Where(x => x.EmployeeId == employeeId);
        }
    }
}
