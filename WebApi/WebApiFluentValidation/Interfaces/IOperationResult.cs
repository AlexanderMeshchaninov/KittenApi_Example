using System.Collections.Generic;

namespace WebApiFluentValidation.Interfaces
{
    public interface IOperationResult<TResult> 
        where TResult : class
    {
        TResult Result { get; }
        IReadOnlyList<IOperationFailure> Failures { get; }
        bool Succeed { get; }
    }
}