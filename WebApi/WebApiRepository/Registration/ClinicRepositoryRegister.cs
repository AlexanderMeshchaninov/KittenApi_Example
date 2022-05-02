using Microsoft.Extensions.DependencyInjection;

namespace WebApiRepository.Registration
{
    public static class ClinicRepositoryRegister
    {
        public static IServiceCollection RegisterClinicRepository(this IServiceCollection services)
        {
            return services.AddTransient<IClinicsRepository, ClinicRepository>();
        }
    }
}