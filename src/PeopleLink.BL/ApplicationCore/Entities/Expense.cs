using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace ApplicationCore.Entities
{
    public abstract class Expense : BaseEntity
    {
        public Expense(ExpenseType expenseType)
        {
            ExpenseType = expenseType;
        }

        public string EmployeeId { get; set; } = null!;
        public ExpenseType ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string Status { get; set; } = ApprovalStatus.WaitingApproved;
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public DateTime? ResponseDate { get; set; }
        public string DocumentUri { get; set; } = null!;

    }
}


