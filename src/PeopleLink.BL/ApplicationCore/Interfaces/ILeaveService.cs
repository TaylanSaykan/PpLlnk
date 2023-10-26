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
    public interface ILeaveService
    {
        Task<List<Leave>> GetLeavesAsync();

        Task<List<Leave>> GetLeavesAsync(ISpecification<Leave> specification);

        Task<Leave> GetLeaveAsync(int id);

        Task<Leave> CreateLeaveAsync(string employeeId, DateTime startingDate, DateTime endingDate, LeaveType type);

        Task DeleteAsync(int leaveId);
        Task UpdateAsync(Leave leave);
    }
}
