using System.Collections.Generic;
using WebApiFluentValidation.Interfaces;

namespace WebApiFluentValidation.Models
{
    public sealed class OperationClinicServiceResult : IOperationResult<ClinicServiceRequestValidation>
    {
        public ClinicServiceRequestValidation Result { get; }
        public IReadOnlyList<IOperationFailure> Failures { get; }
        public bool Succeed { get; }

        public OperationClinicServiceResult(ClinicServiceRequestValidation result)
        {
            Result = result;
            Succeed = true;
        }
        public OperationClinicServiceResult(IReadOnlyList<IOperationFailure> failures)
        {
            Failures = failures;
        }
    }
}