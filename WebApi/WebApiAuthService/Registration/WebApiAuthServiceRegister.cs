using Microsoft.Extensions.DependencyInjection;

namespace WebApiAuthService.Registration
{
    public static class WebApiAuthServiceRegister
    {
        public static IServiceCollection RegisterWebApiAuthService(this IServiceCollection services)
        {
            return services.AddTransient<IAuthService, WebApiAuthService>();
        }
    }
}