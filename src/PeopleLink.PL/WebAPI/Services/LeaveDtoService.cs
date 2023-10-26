using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications.LeaveSpecifications;
using WebAPI.Dtos;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class LeaveDtoService : ILeaveDtoService
    {
        private readonly ILeaveService _leaveService;

        public LeaveDtoService(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        public async Task<List<LeaveDto>> GetAllLeaveDtos(string? approvalStatus, string employeeId)
        {
            var specLeaves = new LeaveApprovalSpecification(approvalStatus, employeeId);
            var leaves = await _leaveService.GetLeavesAsync(specLeaves);

            var leavesDto = leaves.Select(l => new LeaveDto()
            {
                Id = l.Id,
                EmployeeId = l.EmployeeId,
                StartingDate = l.StartingDate,
                EndingDate = l.EndingDate,
                LeaveType = l.LeaveType,
                RequestDate = l.RequestDate,
                ApprovalDate = l.ApprovalDate,
                TotalDays = l.TotalDays,
                Status = l.Status
            }).ToList();

            return leavesDto;
        }

        public async Task<Leave> GetLeave(int leaveId)
        {
            var leave = await _leaveService.GetLeaveAsync(leaveId);

            return leave;
        }

        public async Task<Leave> PostLeave(PostLeaveDto model)
        {
            var leave = await _leaveService.CreateLeaveAsync(model.EmployeeId, model.StartingDate, model.EndingDate, model.LeaveType);

            return leave;
        }

        public async Task DeleteLeaveAsync(int leaveId)
        {
            await _leaveService.DeleteAsync(leaveId);
        }

        public async Task UpdateAsync(LeaveDto leave)
        {
            var updatedLeave = await _leaveService.GetLeaveAsync(leave.Id);

            //updatedLeave.Id = leave.Id;
            //updatedLeave.EmployeeId = leave.EmployeeId;
            //updatedLeave.StartingDate = leave.StartingDate;
            //updatedLeave.EndingDate = leave.EndingDate;
            //updatedLeave.RequestDate = leave.RequestDate;
            //updatedLeave.TotalDays = leave.TotalDays;
            //updatedLeave.LeaveType = leave.LeaveType;
            //updatedLeave.RequestDate = leave.RequestDate;

            updatedLeave.Status = leave.Status;
            updatedLeave.ApprovalDate = leave.ApprovalDate;

            await _leaveService.UpdateAsync(updatedLeave);
        }
    }
}
