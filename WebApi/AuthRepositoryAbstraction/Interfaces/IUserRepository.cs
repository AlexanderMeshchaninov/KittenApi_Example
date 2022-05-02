using System.Threading.Tasks;

namespace AuthRepositoryAbstraction.Interfaces
{
    public interface IUserRepository<T> where T : class
    {  
        Task<Task> CreateUserAsync(T item);
    }
}