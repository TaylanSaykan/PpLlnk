using ApplicationCore.Entities;
using ApplicationCore.Enums;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdvanceService
    {
        Task<List<Advance>> GetAllAdvancesAsync();
        Task<List<Advance>> GetAllAdvancesAsync(ISpecification<Advance> specification);
        Task<Advance> GetAdvanceByIdAsync(int id);
        Task<Advance> CreateAdvanceAsync(string employeeId, decimal amount, Currency currency, AdvanceType type, string description);
        Task DeleteAsync(int id);
        Task UpdateAsync(Advance advance);
    }
}
