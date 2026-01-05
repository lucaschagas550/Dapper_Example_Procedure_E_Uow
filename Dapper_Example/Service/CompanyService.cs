using DapperExample.Models;
using DapperExample.Repository.Interfaces;
using DapperExample.Service.Interfaces;

namespace DapperExample.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository=repository;
        }

        public async Task<List<Company>> GetCompanies()
        {
            return await _repository.GetCompanies();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            return await _repository.GetCompanyById(id).ConfigureAwait(false);
        }

        public async Task<Company> GetCompanyByIdWithParameters(int id, string name)
        {
            return await _repository.GetCompanyByIdWithParameters(id, name).ConfigureAwait(false);
        }

        public async Task<int> CreateCompany(Company company)
        {
            return await _repository.CreateCompany(company).ConfigureAwait(false);
        }

        public async Task UpdateComapny(Company company)
        {
            await _repository.UpdateCompany(company).ConfigureAwait(false);
        }
    }
}
