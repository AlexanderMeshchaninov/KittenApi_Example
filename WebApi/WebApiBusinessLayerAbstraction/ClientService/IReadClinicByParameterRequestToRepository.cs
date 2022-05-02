using System.Threading.Tasks;
using WebApiBusinessLayerAbstraction.Requests;

namespace WebApiBusinessLayerAbstraction.ClientService
{
    public interface IReadClinicByParameterRequestToRepository<T> where T : class
    {
        Task<T> ReadClinicByParameterRequestToRepositoryAsync(IRequestClinicsFromWebApiDto item);
    }
}