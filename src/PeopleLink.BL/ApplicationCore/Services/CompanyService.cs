using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _companyRepo;

        public CompanyService(IRepository<Company> companyRepo)
        {
            _companyRepo = companyRepo;
        }

        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _companyRepo.GetAllAsync();
        }

        public async Task<List<Company>> GetCompaniesAsync(ISpecification<Company> specification)
        {
            return await _companyRepo.GetAllAsync(specification);
        }

        public async Task<Company> GetCompanyAsync(int id)
        {
            var company = await _companyRepo.GetByIdAsync(id);
            return company!;
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            return await _companyRepo.AddAsync(company);
        }

        public async Task DeleteCompanyAsync(int companyId)
        {
            var company = await _companyRepo.GetByIdAsync(companyId);
            await _companyRepo.DeleteAsync(company!);
        }

        public async Task UpdateCompanyAsync(Company company)
        {
            await _companyRepo.UpdateAsync(company);
        }
    }
}
