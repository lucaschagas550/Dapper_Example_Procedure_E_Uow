using DapperExample.Repository;
using DapperExample.Repository.Interfaces;
using DapperExample.Repository.Repositories;
using DapperExample.Repository.Uow;
using DapperExample.Service;
using DapperExample.Service.Interfaces;

namespace DapperExample.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //services.AddSingleton<AppDbContext>();

            services.AddScoped<DbContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<ICompanyService, CompanyService>();
        }
    }
}
