using System.Collections.Generic;

namespace AuthApiFluentValidationAbstraction.Interfaces
{
    public interface IOperationResult<TResult> 
        where TResult : class
    {
        TResult Result { get; }
        IReadOnlyList<IOperationFailure> Failures { get; }
        bool Succeed { get; }
    }
}