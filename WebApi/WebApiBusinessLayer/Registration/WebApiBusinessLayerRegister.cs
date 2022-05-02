using Microsoft.Extensions.DependencyInjection;

namespace WebApiBusinessLayer.Registration
{
    public static class WebApiBusinessLayerRegister
    {
        public static IServiceCollection RegisterWebApiBusinessLayer(this IServiceCollection services)
        {
            return services
                .AddTransient<IClinicService, ClientClinicService>()
                .AddTransient<IKittenService, ClientKittenService>();
        }
    }
}