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
    public interface ICompanyService
    {
        Task<List<Company>> GetCompaniesAsync();
        Task<List<Company>> GetCompaniesAsync(ISpecification<Company> specification);
        Task<Company> GetCompanyAsync(int id);
        Task<Company> CreateCompanyAsync(Company company);
        Task DeleteCompanyAsync(int id);
        Task UpdateCompanyAsync(Company company);
    }
}
