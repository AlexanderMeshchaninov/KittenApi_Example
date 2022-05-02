using System.Threading.Tasks;

namespace WebApiBusinessLayerAbstraction.ClientService
{
    public interface IClientMedicalService<T> where T : class
    {
        Task<Task> MedicalProcedureRequestAsync(T item);
    }
}