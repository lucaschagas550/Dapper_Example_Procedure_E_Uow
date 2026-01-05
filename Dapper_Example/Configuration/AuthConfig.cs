namespace DapperExample.Configuration
{
    public static class AuthConfig
    {
        public static void UseAuthConfiguration(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
