using System.Threading.Tasks;

namespace AuthServiceAbstraction.AuthServiceInterfaces
{
    public interface IAuthIServiceInterface<T> 
        where T : class
    {
        Task<T> AuthenticateAsync(string email, string password);
    }
}