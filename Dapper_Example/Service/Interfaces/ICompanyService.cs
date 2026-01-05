using DapperExample.Models;

namespace DapperExample.Service.Interfaces
{
    public interface ICompanyService
    {
        Task<List<Company>> GetCompanies();
        Task<Company> GetCompanyById(int id);
        Task<Company> GetCompanyByIdWithParameters(int id, string name);
        Task<int> CreateCompany(Company company);
        Task UpdateComapny(Company company);
    }
}
