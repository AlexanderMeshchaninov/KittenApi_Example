using System.Threading.Tasks;

namespace WebApiRepositoryAbstraction.Interfaces
{
    public interface IKittenRepository<T> where T : class
    {
        Task<Task> CreateAsync(T item);
        Task<Task> UpdateAsync(int id, T item);
        Task<Task> UpdateKittenMedicalInspectionAsync(int id, T item);
        Task<Task> DeleteAsync(int id);
    }
}