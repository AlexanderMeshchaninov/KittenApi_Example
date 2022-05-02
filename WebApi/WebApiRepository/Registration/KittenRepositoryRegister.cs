using Microsoft.Extensions.DependencyInjection;

namespace WebApiRepository.Registration
{
    public static class KittenRepositoryRegister
    {
        public static IServiceCollection RegisterKittenRepository(this IServiceCollection services)
        {
            return services.AddTransient<IKittensRepository, KittenRepository>();
        }
    }
}