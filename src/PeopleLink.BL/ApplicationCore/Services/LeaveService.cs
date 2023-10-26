using ApplicationCore.Entities;
using ApplicationCore.Entities.LeaveEntities;
using ApplicationCore.Enums;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly IRepository<Leave> _leaveRepo;

        public LeaveService(IRepository<Leave> leaveRepo)
        {
            _leaveRepo = leaveRepo;
        }

        public async Task<List<Leave>> GetLeavesAsync()
        {
            return await _leaveRepo.GetAllAsync();
        }

        public Task<List<Leave>> GetLeavesAsync(ISpecification<Leave> specification)
        {
            return _leaveRepo.GetAllAsync(specification);
        }

        public async Task<Leave> GetLeaveAsync(int id)
        {
            var leave = await _leaveRepo.GetByIdAsync(id);

            return leave!;
        }

        public async Task<Leave> CreateLeaveAsync(string employeeId, DateTime startingDate, DateTime endingDate, LeaveType type)
        {
            Leave leave;

            switch (type)
            {
                case LeaveType.AnnualLeave:
                    leave = new AnnualLeave(startingDate, endingDate);
                    break;
                case LeaveType.BereavementLeave:
                    leave = new BereavementLeave(startingDate);
                    break;
                case LeaveType.CompassionateLeave:
                    leave = new CompassionateLeave(startingDate, endingDate);
                    break;
                case LeaveType.MarriageLeave:
                    leave = new MarriageLeave(startingDate);
                    break;
                case LeaveType.MaternityLeave:
                    leave = new MaternityLeave(startingDate);
                    break;
                case LeaveType.PaternityLeave:
                    leave = new PaternityLeave(startingDate);
                    break;
                case LeaveType.SickLeave:
                    leave = new SickLeave(startingDate, endingDate);
                    break;
                default:
                    throw new LeaveTypeException();
            }

            leave.EmployeeId = employeeId;

            return await _leaveRepo.AddAsync(leave);
        }

        public async Task DeleteAsync(int leaveId)
        {
            var leave = await _leaveRepo.GetByIdAsync(leaveId);

            await _leaveRepo.DeleteAsync(leave!);
        }

        public async Task UpdateAsync(Leave leave)
        {
            var updatedLeave = await _leaveRepo.GetByIdAsync(leave.Id);

            await _leaveRepo.UpdateAsync(updatedLeave);
 
        }
    }
}
