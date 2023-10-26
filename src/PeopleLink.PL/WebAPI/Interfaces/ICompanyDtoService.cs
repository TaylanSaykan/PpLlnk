using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Dtos;

namespace WebAPI.Interfaces
{
    public interface ICompanyDtoService
    {
        Task<List<CompanyDto>> GetAllCompanyDtos();

        Task<Company> GetCompany(int companyId);

        Task<Company> PostCompany(PostCompanyDto companyDto);

        Task UpdateCompany(CompanyDto companyDto);

        Task DeleteCompany(int companyId);
    }
}
