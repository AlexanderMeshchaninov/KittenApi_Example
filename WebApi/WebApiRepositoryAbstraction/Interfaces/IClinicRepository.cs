using System.Threading.Tasks;

namespace WebApiRepositoryAbstraction.Interfaces
{
    public interface IClinicRepository<T> where T : class
    {
        Task<Task> CreateClinicAsync(T item);
        Task<Task> CreateKittenToClinicAsync(int clinicId, int kittenId);
        Task<Task> UpdateAsync(int id, string newClinicName);
        Task<Task> DeleteAsync(int id);
    }
}