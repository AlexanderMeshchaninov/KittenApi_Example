using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthDataLayer;
using AuthDataLayer.Models;
using AuthRepositoryAbstraction.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthRepository
{
    public interface IUserRepository : 
        IUserRepository<User>, 
        IReadUserRepository<User>, 
        IUserTokenRepository
    {
    }

    public sealed class AuthRepository : IUserRepository
    {
        private readonly ILogger<AuthRepository> _logger;
        private readonly AuthDataContext _context;

        public AuthRepository(
            ILogger<AuthRepository> logger, 
            AuthDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Task> CreateUserAsync(User user)
        {
            using (var db = _context)
            {
                try
                {
                    await db.Clients.AddAsync(user);
                    await db.SaveChangesAsync();

                    _logger.LogInformation(
                        $"User has been added to DB with id: {user.Id.ToString()}, email: {user.Email}");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return Task.FromException(new NullReferenceException());
                }
            }
            return Task.CompletedTask;
        }

        public async Task<Task> CreateUserTokensAsync(
            string email, 
            string token, 
            string refreshToken)
        {
            using (var db = _context)
            {
                try
                {
                    var currentClient = await db.Clients
                        .Where(x => x.Email.Equals(email))
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    currentClient.Token = token;
                    currentClient.RefreshToken = refreshToken;

                    db.Clients.Update(currentClient);

                    await db.SaveChangesAsync();

                    _logger.LogInformation($"Token has been added to DB");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return Task.FromResult(ex);
                }
            }
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyCollection<User>> ReadUserInfoAsync(string email, string password)
        {
            if (email != null && password != null)
            {
                try
                {
                    return await _context.Clients
                        .Where(x => x.Email.Contains(email) && x.Password.Contains(password))
                        .AsNoTracking()
                        .ToArrayAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception:{ex}");
                    return null;
                }
            }
            return null;
        }

        public async Task<IReadOnlyCollection<User>> ReadUserTokenAsync(string userToken)
        {
            using (var db = _context)
            {
                if (userToken != null)
                {
                    try
                    {
                        return await db.Clients
                            .Where(x => x.RefreshToken.Contains(userToken))
                            .AsNoTracking()
                            .ToArrayAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Exception:{ex}");
                        return null;
                    }
                }
                return null;
            }
        }
    }
}
