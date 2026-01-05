using Dapper;
using DapperExample.Models;
using DapperExample.Repository.Interfaces;

namespace DapperExample.Repository.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetCompanies()
        {
            try
            {
                var query = "SELECT * FROM Company";

                using var connection = _context.CreateConnection();
                var companies = await connection.QueryAsync<Company>(query).ConfigureAwait(false);

                return companies.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Company> GetCompanyById(int id)
        {
            try
            {
                var query = "SELECT * FROM Company WHERE Id = @Id";

                using var connection = _context.CreateConnection();
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id }).ConfigureAwait(false);

                return company ?? new Company();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Company> GetCompanyByIdWithParameters(int id, string name)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id);

                var query = "SELECT * FROM Company WHERE Id = @Id";

                if (!string.IsNullOrEmpty(name))
                {
                    query += " AND Name = @Name";
                    parameters.Add("Name", name);
                }

                using var connection = _context.CreateConnection();

                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, parameters).ConfigureAwait(false);

                return company ?? new Company();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> CreateCompany(Company company)
        {
            try
            {
                var query = "INSERT INTO Company (Name, Address, Country) VALUES (@Name, @Address, @Country)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

                using var connection = _context.CreateConnection();

                var id = await connection.QuerySingleAsync<int>(query,
                    new
                    {
                        Name = company.Name,
                        Address = company.Address,
                        Country = company.Country
                    }).ConfigureAwait(false);

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCompany(Company company)
        {
            try
            {
                var query = "UPDATE Company SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";
                using var connection = _context.CreateConnection();

                await connection.ExecuteAsync(query,
                    new
                    {
                        Id = company.Id,
                        Name = company.Name,
                        Address = company.Address,
                        Country = company.Country
                    });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
