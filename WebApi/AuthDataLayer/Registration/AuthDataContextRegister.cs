using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthDataLayer.Registration
{
    public static class AuthDataContextRegister
    {
        public static IServiceCollection RegisterAuthDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<AuthDataContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        }
    }
}