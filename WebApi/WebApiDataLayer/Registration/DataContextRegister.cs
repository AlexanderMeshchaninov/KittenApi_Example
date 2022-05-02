using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiDataLayer.Registration
{
    public static class DataContextRegister
    {
        public static IServiceCollection RegisterDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<WebApiDataContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"));
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
        }
    }
}