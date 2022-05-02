using System.Threading.Tasks;

namespace AuthServiceAbstraction.AuthServiceInterfaces
{
    public interface IAuthGenerateRefreshTokenInterface<T> 
        where T : class
    {
        public T GenerateRefreshToken(int id);
        public Task<string> RefreshTokenAsync(string token);
    }
}