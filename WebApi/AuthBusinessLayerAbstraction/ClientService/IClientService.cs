using System.Threading.Tasks;
using AuthService.TokenResponses;

namespace AuthBusinessLayerAbstraction.ClientService
{
    public interface IClientService<T> 
        where T : class
    {
        Task<Task> RegisterUserRequestToRepositoryAsync(T item);
        Task<TokenResponse> AuthenticationRequestAsync(T item);
        Task<string> RefreshTokenResponseAsync(string oldRefreshToken);
    }
}