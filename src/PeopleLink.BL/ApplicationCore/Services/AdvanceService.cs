using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using ApplicationCore.Exceptions;
using ApplicationCore.Entities;
using ApplicationCore.Entities.AdvanceEntities;

namespace ApplicationCore.Services
{
    public class AdvanceService : IAdvanceService
    {
        private readonly IRepository<Advance> _advanceRepo;

        public AdvanceService(IRepository<Advance> advanceRepo)
        {
            _advanceRepo = advanceRepo;
        }

        public async Task<List<Advance>> GetAllAdvancesAsync()
        {
            return await _advanceRepo.GetAllAsync();
        }

        public async Task<List<Advance>> GetAllAdvancesAsync(ISpecification<Advance> specification)
        {
            return await _advanceRepo.GetAllAsync(specification);
        }

        public async Task<Advance> GetAdvanceByIdAsync(int id)
        {
            return await _advanceRepo.GetByIdAsync(id);
        }

        public async Task<Advance> CreateAdvanceAsync(string employeeId, decimal amount, Currency currency, AdvanceType type, string description)
        {
            Advance advance;

            switch (type)
            {
                case AdvanceType.Individual:
                    advance = new IndividualAdvance();

                    break;
                case AdvanceType.Corporate:
                    advance = new CorporateAdvance();
                    break;
                default:
                    throw new AdvanceTypeException();
            };

            advance.EmployeeId = employeeId;
            advance.Amount = amount;
            advance.Currency = currency;
            advance.Description = description;

            return await _advanceRepo.AddAsync(advance);
        }

        public async Task DeleteAsync(int advanceId)
        {
            var advance = await _advanceRepo.GetByIdAsync(advanceId);
            await _advanceRepo.DeleteAsync(advance);
        }

        public async Task UpdateAsync(Advance advance)
        {
            var updatedAdvance = await _advanceRepo.GetByIdAsync(advance.Id);

            await _advanceRepo.UpdateAsync(updatedAdvance);
        }
    }
}
