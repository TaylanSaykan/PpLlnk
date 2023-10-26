using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.ExpenseEntities
{
    public class AccommodationExpense : Expense
    {
        public AccommodationExpense() : base(ExpenseType.Accommodation)
        {
        }
    }
}
