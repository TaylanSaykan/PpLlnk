using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyDtoService _companyDtoService;

        public CompanyController(ICompanyDtoService companyDtoService)
        {
            _companyDtoService = companyDtoService;
        }

        [HttpGet]
        public async Task<List<CompanyDto>> GetCompanies()
        {
            var companies = await _companyDtoService.GetAllCompanyDtos();
            return companies;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyDtoService.GetCompany(id);

            if (company == null)
                return NotFound();

            return company;
        }

        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(PostCompanyDto companyDto)
        {
            if (ModelState.IsValid)
            {
                var createdCompany = await _companyDtoService.PostCompany(companyDto);
                return CreatedAtAction(nameof(GetCompany), new { id = createdCompany.Id }, createdCompany);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(CompanyDto companyDto)
        {
            var company = await _companyDtoService.GetCompany(companyDto.Id);
            if (company == null)
                return NotFound();

            await _companyDtoService.UpdateCompany(companyDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var companyDto = await _companyDtoService.GetCompany(id);

            if (companyDto == null)
                return NotFound();

            await _companyDtoService.DeleteCompany(id);

            return NoContent();
        }
    }
}
