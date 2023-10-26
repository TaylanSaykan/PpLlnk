using ApplicationCore.Entities;
using ApplicationCore.Entities.AdvanceEntities;
using ApplicationCore.Enums;
using Ardalis.Specification;

namespace ApplicationCore.Specifications.AdvanceSpecifications
{
    public class AdvanceAmountFilterSpecification : Specification<Advance>
    {
        public AdvanceAmountFilterSpecification(AdvanceType advanceType, decimal employeeSalary)
        {
            switch (advanceType)
            {
                case AdvanceType.Individual:
                    Query.Where(x => x.AdvanceType == AdvanceType.Individual &&
                                    (x as IndividualAdvance).CanRequestAdvance(x.Amount, employeeSalary));
                    break;
                case AdvanceType.Corporate:
                    // Kurumsal avansta herhangi bir sınırlama yok
                    Query.Where(x => x.AdvanceType == AdvanceType.Corporate);
                    break;
            }
        }
    }
}
