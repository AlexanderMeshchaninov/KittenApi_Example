using System.Collections.Generic;
using AuthApiFluentValidationAbstraction.Interfaces;

namespace AuthApiFluentValidation.Models
{
    public sealed class OperationResult : IOperationResult<UserRequestValidation>
    {
        public UserRequestValidation Result { get; }
        public IReadOnlyList<IOperationFailure> Failures { get; }
        public bool Succeed { get; }
        public OperationResult(UserRequestValidation result)
        {
            Result = result;
            Succeed = true;
        }
        public OperationResult(IReadOnlyList<IOperationFailure> failures)
        {
            Failures = failures;
        }
    }
}