using System.Collections.Generic;
using WebApiFluentValidation.Interfaces;

namespace WebApiFluentValidation.Models
{
    public class OperationKittenResult : IOperationResult<KittenRequestValidation>
    {
        public KittenRequestValidation Result { get; }
        public IReadOnlyList<IOperationFailure> Failures { get; }
        public bool Succeed { get; }
        public OperationKittenResult(KittenRequestValidation result)
        {
            Result = result;
            Succeed = true;
        }
        public OperationKittenResult(IReadOnlyList<IOperationFailure> failures)
        {
            Failures = failures;
        }
    }
}