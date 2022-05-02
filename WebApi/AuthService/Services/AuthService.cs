using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthRepository;
using AuthService.Models;
using AuthService.TokenResponses;
using AuthServiceAbstraction.AuthServiceInterfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services
{
    public interface IAuthService : 
        IAuthIServiceInterface<TokenResponse>, 
        IAuthGenerateJwtTokenInterface, 
        IAuthGenerateRefreshTokenInterface<RefreshToken>
    {
    }

    public sealed class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthResponse _authResponse;
        private readonly IClientDictionaryInterface<AuthResponse> _users;
        private readonly ILogger<AuthService> _logger;

        public const string SecretCode = "THIS IS SOME VERY SECRET STRING!!! Im blue da ba dee da ba " +
                                         "di da ba dee da ba di da d ba dee da ba di da ba dee";

        public AuthService(
            IUserRepository userRepository, 
            IClientDictionaryInterface<AuthResponse> users, 
            AuthResponse authResponse,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _users = users;
            _authResponse = authResponse;
            _logger = logger;
        }

        public async Task <TokenResponse> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return new TokenResponse()
                {
                    Token = string.Empty
                };
            }

            try
            {
                var currentUser = await _userRepository.ReadUserInfoAsync(email, password);
                var users = _users.ClientDictionary(currentUser.FirstOrDefault()?.Email,
                    _authResponse.Password = currentUser.FirstOrDefault()?.Password);

                TokenResponse tokenResponse = new TokenResponse();

                int i = 0;

                foreach (KeyValuePair<string, AuthResponse> pair in users)
                {
                    i++;

                    if (string.CompareOrdinal(pair.Key, email) == 0
                        && string.CompareOrdinal(pair.Value.Password, password) == 0)
                    {
                        tokenResponse.Token = await Task.Run(() => GenerateJwtToken(i, 15));

                        RefreshToken refreshToken = await Task.Run(() => GenerateRefreshToken(i));

                        _authResponse.LatestRefreshToken = refreshToken;

                        tokenResponse.RefreshToken = refreshToken.Token;

                        await _userRepository.CreateUserTokensAsync(email, tokenResponse.Token, tokenResponse.RefreshToken);

                        return tokenResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception {ex}");
            }

            return null;
        }

        public async Task<string> RefreshTokenAsync(string token)
        {
            var currentUser = await _userRepository.ReadUserTokenAsync(token);
            var users = _users.ClientDictionary(currentUser.SingleOrDefault().Email, _authResponse.Password);

            int i = 0;

            foreach (KeyValuePair<string, AuthResponse> pair in users)
            {
                i++;

                if (string.CompareOrdinal(_authResponse.LatestRefreshToken.Token, token) == 0 
                    && _authResponse.LatestRefreshToken.IsExpired is false)
                {
                    pair.Value.LatestRefreshToken = await Task.Run(() => GenerateRefreshToken(i));

                    return pair.Value.LatestRefreshToken.Token;
                }
            }

            return null;
        }

        public RefreshToken GenerateRefreshToken(int id)
        {
            RefreshToken refreshToken = new RefreshToken();

            refreshToken.Expires = DateTime.Now.AddMinutes(360);

            refreshToken.Token = GenerateJwtToken(id, 360);

            return refreshToken;
        }

        public string GenerateJwtToken(int id, int minutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(SecretCode);

            SecurityTokenDescriptor tokerDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),

                Expires = DateTime.UtcNow.AddMinutes(minutes),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokerDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}