using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiRepositoryAbstraction.Interfaces
{
    public interface IFindKittenRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> FindKittenAsync(int id);
    }
}