using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthRepositoryAbstraction.Interfaces
{
    public interface IReadUserRepository<T> where T : class
    {
        Task<IReadOnlyCollection<T>> ReadUserInfoAsync(string email, string password);
        Task<IReadOnlyCollection<T>> ReadUserTokenAsync(string userToken);
    }
}