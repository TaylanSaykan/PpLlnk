using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPI.Services
{
    public class CompanyDtoService : ICompanyDtoService
    {
        private readonly ICompanyService _companyService;

        public CompanyDtoService(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<List<CompanyDto>> GetAllCompanyDtos()
        {
            var companies = await _companyService.GetCompaniesAsync();
            var companyDto = companies.Select(x => new CompanyDto()
            {
                Id = x.Id,
                CompanyName = x.CompanyName,
                CityId = x.CityId,
                CityName = x.CityName,
                DistrictId = x.DistrictId,
                DistrictName = x.DistrictName,
                Address = x.Address,
                Email = x.Email,
                CompanyLogoUrl = x.CompanyLogoUrl,
                ContractStartDate = x.ContractStartDate,
                ContractEndDate = x.ContractEndDate,
                EmployeeCount = x.EmployeeCount,
                EstablishmentYear = x.EstablishmentYear,
                MERSISNumber = x.MERSISNumber,
                PhoneNumber = x.PhoneNumber,
                TaxNumber = x.TaxNumber,
                TaxOffice = x.TaxOffice,
                Title = x.Title,
                IsActive = x.IsActive
            }).ToList();

            return companyDto;
        }

        public async Task<Company> GetCompany(int companyId)
        {
            var company = await _companyService.GetCompanyAsync(companyId);

            return company;

        }
        public Task<Company> PostCompany(PostCompanyDto company)
        {

            Company newCompany = new Company
            {
                CompanyName = company.CompanyName,
                Title = company.Title,
                MERSISNumber = company.MERSISNumber,
                TaxNumber = company.TaxNumber,
                TaxOffice = company.TaxOffice,
                CompanyLogoUrl = company.CompanyLogoUrl,
                PhoneNumber = company.PhoneNumber,
                Address = company.Address,
                CityId = company.CityId,
                CityName = company.CityName,
                DistrictId = company.DistrictId,
                DistrictName = company.DistrictName,
                Email = company.Email,
                EmployeeCount = company.EmployeeCount,
                EstablishmentYear = company.EstablishmentYear,
                ContractStartDate = company.ContractStartDate,
                ContractEndDate = company.ContractEndDate,
            };

            newCompany.IsActive = (company.ContractStartDate > DateTime.Now || company.ContractEndDate < DateTime.Now) ? false : true;

            var createdCompany = _companyService.CreateCompanyAsync(newCompany);
            return createdCompany;
        }

        public async Task DeleteCompany(int companyId)
        {
            await _companyService.DeleteCompanyAsync(companyId);
        }

        public async Task UpdateCompany(CompanyDto companyDto)
        {
            var company = await _companyService.GetCompanyAsync(companyDto.Id);

            company.CompanyName = companyDto.CompanyName;
            company.Title = companyDto.Title;
            company.MERSISNumber = companyDto.MERSISNumber;
            company.TaxNumber = companyDto.TaxNumber;
            company.TaxOffice = companyDto.TaxOffice;
            company.CompanyLogoUrl = companyDto.CompanyLogoUrl;
            company.PhoneNumber = companyDto.PhoneNumber;
            company.Address = companyDto.Address;
            company.CityId = companyDto.CityId;
            company.CityName = companyDto.CityName;
            company.DistrictId = companyDto.DistrictId;
            company.DistrictName = companyDto.DistrictName;
            company.Email = companyDto.Email;
            company.EmployeeCount = companyDto.EmployeeCount;
            company.EstablishmentYear = companyDto.EstablishmentYear;
            company.ContractStartDate = companyDto.ContractStartDate;
            company.ContractEndDate = companyDto.ContractEndDate;
            company.IsActive = companyDto.IsActive;

            await _companyService.UpdateCompanyAsync(company);
        }
    }
}
