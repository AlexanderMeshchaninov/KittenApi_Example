using AuthService.Services;
using AuthService.TokenResponses;
using AuthServiceAbstraction.AuthServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Registration
{
    public static class AuthServiceRegister
    {
        public static IServiceCollection RegisterAuthService(this IServiceCollection services)
        {
            return services
                .AddSingleton<AuthResponse>()
                .AddTransient<IAuthService, Services.AuthService>()
                .AddTransient<IClientDictionaryInterface<AuthResponse>, ClientDict>();
        }
    }
}