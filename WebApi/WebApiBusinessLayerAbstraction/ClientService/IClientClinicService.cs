using System.Threading.Tasks;

namespace WebApiBusinessLayerAbstraction.ClientService
{
    public interface IClientClinicService<T> where T : class
    {
        Task<Task> RegisterClinicRequestToRepositoryAsync(T item);
        Task<Task> RegisterKittenToClinicRequestToRepositoryAsync(T item);
        Task<Task> UpdateClinicsByIdRequestToRepositoryAsync(T item);
        Task<Task> DeleteClinicsByIdRequestToRepositoryAsync(T item);
    }
}