using Microsoft.Extensions.DependencyInjection;

namespace AuthBusinessLayer.Registration
{
    public static class AuthApiBusinessLayerRegister
    {
        public static IServiceCollection RegisterAuthBusinessLayerClient(this IServiceCollection services)
        {
            return services.AddTransient<IClientService, ClientService>();
        }
    }
}