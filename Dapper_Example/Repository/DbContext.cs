using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperExample.Repository
{
    public sealed class DbContext : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction? Transaction { get; set; }

        private readonly IConfiguration _configuration;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;

            Connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection") ?? "");
            Connection.Open();
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            Connection?.Dispose();
        }
    }
}
