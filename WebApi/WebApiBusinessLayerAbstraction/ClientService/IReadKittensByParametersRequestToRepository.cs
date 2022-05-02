using System.Threading.Tasks;
using WebApiBusinessLayerAbstraction.Requests;

namespace WebApiBusinessLayerAbstraction.ClientService
{
    public interface IReadKittensByParametersRequestToRepository<T> where T : class
    {
        Task<T> ReadKittensByParametersRequestToRepositoryAsync(IRequestKittensFromWebApiDto item);
    }
}