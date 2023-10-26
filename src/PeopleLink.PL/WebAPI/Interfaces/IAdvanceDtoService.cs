using WebAPI.Dtos;

namespace WebAPI.Interfaces
{
    public interface IAdvanceDtoService
    {
        Task<List<AdvanceDto>> GetAllAdvanceDtos(string? status, string employeeId);
        Task<Advance> GetAdvance(int advanceId);
        Task<Advance> PostAdvance(AdvanceDto model);
        Task DeleteAdvanceAsync(int advanceId);

        Task UpdateAsync(AdvanceDto advance);
    }
}
