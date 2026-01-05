using DapperExample.Configuration;

namespace DapperExample
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(Configuration);

            services.AddDbContextConfig(Configuration);

            services.AddSwaggerConfiguration();

            services.RegisterServices();
        }

        public static void Configure(WebApplication app)
        {
            app.UseSwaggerConfiguration();

            app.UseApiConfiguration();
        }
    }
}
