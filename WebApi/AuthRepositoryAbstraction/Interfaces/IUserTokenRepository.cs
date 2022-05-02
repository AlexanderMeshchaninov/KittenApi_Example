using System.Threading.Tasks;

namespace AuthRepositoryAbstraction.Interfaces
{
    public interface IUserTokenRepository
    {
        Task<Task> CreateUserTokensAsync(
            string email, 
            string token, 
            string refreshToken);
    }
}