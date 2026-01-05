using Dapper;
using DapperExample.Models;
using DapperExample.Repository.Interfaces;
using System.Data;

namespace DapperExample.Repository.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DbContext _context;

        public CompanyRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetCompanies()
        {
            var companies = await _context.Connection.QueryAsync<Company>("Test.CompaniesLista_sps", commandType: CommandType.StoredProcedure);

            return companies.ToList();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            var company = await _context.Connection.QuerySingleOrDefaultAsync<Company>("Test.CompaniesObter_sps", new { Id = id }, commandType: CommandType.StoredProcedure);

            return company ?? new Company();
        }

        public async Task<Company> GetCompanyByIdWithParameters(int id, string name)
        {

            var parameters = new { Id = id, Name = name };

            var company = await _context.Connection.QuerySingleOrDefaultAsync<Company>(
                "Test.CompaniesObterFiltroDinamico_sps",
                parameters,
                transaction: _context.Transaction, //Transaction
                commandType: CommandType.StoredProcedure);

            return company ?? new Company();
        }

        public async Task<int> CreateCompany(Company company)
        {
            var parameters = new
            {
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };

            return await _context.Connection.QuerySingleAsync<int>("Test.Companies_spi",
                parameters,
                transaction: _context.Transaction, //Transaction
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateCompany(Company company)
        {
            //Se a transacao for null o dapper executa automaticamente no banco de dados a consulta

            var parameters = new
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };

            await _context.Connection.ExecuteAsync(
                "Test.Companies_spu",
                parameters,
                transaction: _context.Transaction, //Transaction
                commandType: CommandType.StoredProcedure);
        }
    }
}
