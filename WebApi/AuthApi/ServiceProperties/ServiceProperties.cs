using AuthApiAbstraction.Interfaces;

namespace AuthApi.ServiceProperties
{
    public sealed class ServiceProperties : IServiceProviderInterface
    {
        public string Host { get; set; }
    }
}