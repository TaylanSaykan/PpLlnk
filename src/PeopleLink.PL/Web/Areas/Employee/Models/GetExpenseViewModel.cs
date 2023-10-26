using System.ComponentModel.DataAnnotations;
using Web.Areas.Employee.Extensions;

namespace Web.Areas.Employee.Models
{
    public class GetExpenseViewModel
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
      
        public ExpenseType ExpenseType { get; set; }

        public DateTime? ResponseDate { get; set; }

        public DateTime RequestDate { get; set; }
        public string DocumentUri { get; set; }
    }
}
