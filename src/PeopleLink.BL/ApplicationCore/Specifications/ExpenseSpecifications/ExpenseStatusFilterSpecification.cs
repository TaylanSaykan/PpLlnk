using ApplicationCore.Entities;
using ApplicationCore.Enums;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications.ExpenseSpecifications
{
    public class ExpenseStatusFilterSpecification : Specification<Expense>
    {
        public ExpenseStatusFilterSpecification(string? status, string employeeId)
        {
            if (status != null)
            {
                Query.Where(x => x.EmployeeId == employeeId).Where(x => x.Status == status);
            }

            Query.Where(x => x.EmployeeId == employeeId);
        }
    }
}
