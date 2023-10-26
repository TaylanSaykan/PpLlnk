using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveDtoService _leaveDtoService;

        public LeaveController(ILeaveDtoService leaveDtoService)
        {
            _leaveDtoService = leaveDtoService;
        }

        [HttpGet]
        public async Task<List<LeaveDto>> GetLeaves(string? approvalStatus, string employeeId)
        {
            var leaves = await _leaveDtoService.GetAllLeaveDtos(approvalStatus, employeeId);

            return leaves;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Leave>> GetLeave(int id)
        {
            var leave = await _leaveDtoService.GetLeave(id);

            if (leave == null)
                return NotFound();

            return leave;
        }

        [HttpPost]
        public async Task<ActionResult<Leave>> PostLeave(PostLeaveDto leave)
        {
            if (ModelState.IsValid)
            {
                var createdLeave = await _leaveDtoService.PostLeave(leave);

                return CreatedAtAction(nameof(GetLeave), new { id = createdLeave.Id }, createdLeave);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeave(int id)
        {
            var leave = await _leaveDtoService.GetLeave(id);

            if (leave == null)
                return NotFound();

            await _leaveDtoService.DeleteLeaveAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeave(LeaveDto leave)
        {
            if (leave == null)
                return NotFound();

            await _leaveDtoService.UpdateAsync(leave);

            return NoContent();
        }
    }
}
