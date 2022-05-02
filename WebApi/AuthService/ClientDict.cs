using System.Collections.Generic;
using AuthService.TokenResponses;
using AuthServiceAbstraction.AuthServiceInterfaces;

namespace AuthService
{
    public sealed class ClientDict : IClientDictionaryInterface<AuthResponse>
    {
        public Dictionary<string, AuthResponse> ClientDictionary(string email, string password)
        {
            return new Dictionary<string, AuthResponse>()
            {
                {$"{email}", new AuthResponse() {Password = $"{password}"}}
            };
        }
    };
}