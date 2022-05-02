using System.Threading.Tasks;

namespace WebApiFluentValidation.Interfaces
{
    public interface IOperationResultService<TResult> 
        where TResult : class
    {
        Task <IOperationResult<TResult>> StartValidationAsync(TResult request);
    }
}