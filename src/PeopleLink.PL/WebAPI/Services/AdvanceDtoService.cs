using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using ApplicationCore.Specifications.AdvanceSpecifications;
using WebAPI.Dtos;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class AdvanceDtoService : IAdvanceDtoService
    {
        private readonly IAdvanceService _advanceService;

        public AdvanceDtoService(IAdvanceService advanceService)
        {
            _advanceService = advanceService;
        }

        public async Task<List<AdvanceDto>> GetAllAdvanceDtos(string? status, string employeeId)
        {
            var specAdvances = new AdvanceStatusSpecification(status, employeeId);
            var advances = await _advanceService.GetAllAdvancesAsync(specAdvances);

            var advancevm = advances.Select(a => new AdvanceDto()
            {
                Id = a.Id,
                EmployeeId = a.EmployeeId,
                AdvanceType = a.AdvanceType,
                Amount = a.Amount,
                Currency = a.Currency,
                Status = a.Status,
                RequestDate = a.RequestDate,
                ResponseDate = a.ResponseDate,
                Description = a.Description
            }).ToList();

            return advancevm;
        }

        public async Task<Advance> GetAdvance(int advanceId)
        {
            var advance = await _advanceService.GetAdvanceByIdAsync(advanceId);

            return advance;
        }
        public async Task<Advance> PostAdvance(AdvanceDto model)
        {
            var createdAdvance = await _advanceService.CreateAdvanceAsync(model.EmployeeId, model.Amount, model.Currency, model.AdvanceType, model.Description);

            return createdAdvance;
        }

        public async Task DeleteAdvanceAsync(int advanceId)
        {
            await _advanceService.DeleteAsync(advanceId);
        }

        public async Task UpdateAsync(AdvanceDto advance)
        {
            var updatedAdvance = await _advanceService.GetAdvanceByIdAsync(advance.Id);
            updatedAdvance.Status = advance.Status;
            updatedAdvance.ResponseDate = advance.ResponseDate;

            await _advanceService.UpdateAsync(updatedAdvance);
        }
    }
}
