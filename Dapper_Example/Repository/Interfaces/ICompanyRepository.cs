using DapperExample.Models;

namespace DapperExample.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetCompanies();
        Task<Company> GetCompanyById(int id);
        Task<Company> GetCompanyByIdWithParameters(int id, string name);
        Task<int> CreateCompany(Company company);
        Task UpdateCompany(Company company);
    }
}
