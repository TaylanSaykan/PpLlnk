using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvanceController : Controller
    {
        private readonly IAdvanceDtoService _advanceDtoService;

        public AdvanceController(IAdvanceDtoService advanceDtoService)
        {
            _advanceDtoService = advanceDtoService;
        }

        [HttpGet]
        public async Task<List<AdvanceDto>> GetAdvances(string? status, string employeeId)
        {
            var advances = await _advanceDtoService.GetAllAdvanceDtos(status, employeeId);
            return advances;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Advance>> GetAdvance(int id)
        {
            var advance = await _advanceDtoService.GetAdvance(id);

            if (advance == null)
                return NotFound();

            return advance;
        }

        [HttpPost]
        public async Task<ActionResult<AdvanceDto>> PostAdvance(AdvanceDto advance)
        {
            if (ModelState.IsValid)
            {
                var createdAdvance = await _advanceDtoService.PostAdvance(advance);
                return CreatedAtAction(nameof(GetAdvance), new { id = createdAdvance.Id }, createdAdvance);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvance(int id)
        {
            var advance = await _advanceDtoService.GetAdvance(id);

            if (advance == null)
                return NotFound();

            await _advanceDtoService.DeleteAdvanceAsync(id);

            return NoContent();
        }

    }
}
