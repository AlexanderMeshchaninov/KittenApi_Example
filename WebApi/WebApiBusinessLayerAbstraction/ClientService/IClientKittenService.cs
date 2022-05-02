using System.Threading.Tasks;

namespace WebApiBusinessLayerAbstraction.ClientService
{
    public interface IClientKittenService<T> where T : class
    {
        Task<Task> RegisterKittenRequestToRepositoryAsync(T item);
        Task<Task> UpdateKittenByIdRequestToRepositoryAsync(int id, T item);
        Task<Task> DeleteKittenByIdRequestToRepositoryAsync(T item);
    }
}