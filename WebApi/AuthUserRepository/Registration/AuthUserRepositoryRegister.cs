using Microsoft.Extensions.DependencyInjection;

namespace AuthRepository.Registration
{
    public static class AuthUserRepositoryRegister
    {
        public static IServiceCollection RegisterUserRepository(this IServiceCollection services)
        {
            return services.AddTransient<IUserRepository, AuthRepository>();
        }
    }
}