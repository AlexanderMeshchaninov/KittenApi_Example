using System.Threading.Tasks;
using AuthBusinessLayer.Requests;
using AuthBusinessLayerAbstraction.ClientService;
using AuthDataLayer.Models;
using AuthRepository;
using AuthService.Services;
using AuthService.TokenResponses;

namespace AuthBusinessLayer
{
    public interface IClientService : 
        IClientService<RequestFromAuthApiDto>
    {
    }

    public sealed class ClientService : IClientService
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public ClientService(
            IUserRepository repository,
            IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public async Task<Task> RegisterUserRequestToRepositoryAsync(RequestFromAuthApiDto item)
        {
            var newUser = new User()
            {
                Id = 0,
                UserName = item.UserName, 
                Email = item.Email, 
                Password = item.Password
            };

            var res = await _repository.CreateUserAsync(newUser);
            if (res.Exception is not null)
            {
                return Task.FromException(res.Exception);
            }

            return Task.CompletedTask;
        }

        public async Task<TokenResponse> AuthenticationRequestAsync(RequestFromAuthApiDto item)
        {
            var token = await _authService.AuthenticateAsync(item.Email, item.Password);
            if (token is null)
            {
                return null;
            }
            return token = new TokenResponse()
            {
                Token = token.Token,
                RefreshToken = token.RefreshToken
            };
        }

        public async Task<string> RefreshTokenResponseAsync(string oldRefreshToken)
        {
            return await _authService.RefreshTokenAsync(oldRefreshToken);
        }
    }
}