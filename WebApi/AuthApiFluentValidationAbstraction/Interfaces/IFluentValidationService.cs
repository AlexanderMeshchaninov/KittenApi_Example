using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthApiFluentValidationAbstraction.Interfaces
{
    public interface IFluentValidationService<TEntity> 
        where TEntity : class
    {
        Task <IReadOnlyList<IOperationFailure>> ValidateEntityAsync(TEntity item);
    }
}