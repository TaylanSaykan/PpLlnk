using System;
using ApplicationCore.Enums;

namespace ApplicationCore.Entities.AdvanceEntities
{
    public class IndividualAdvance : Advance
    {
        public IndividualAdvance() : base(AdvanceType.Individual)
        {
        }

        internal bool CanRequestAdvance(decimal amount, decimal employeeSalary)
        {
            decimal maxAllowedAmount = employeeSalary * 3;
            return amount <= maxAllowedAmount;
        }
    }
}
