using WebAPI.Dtos;

namespace WebAPI.Interfaces
{
    public interface ILeaveDtoService
    {
        Task<List<LeaveDto>> GetAllLeaveDtos(string? approvalStatus, string employeeId);

        Task<Leave> GetLeave(int leaveId);

        Task<Leave> PostLeave(PostLeaveDto model);

        Task DeleteLeaveAsync(int leaveId);

        Task UpdateAsync(LeaveDto leave);
    }
}
