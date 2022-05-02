using System.Collections.Generic;
using WebApiFluentValidation.Interfaces;

namespace WebApiFluentValidation.Models
{
    public sealed class OperationClinicResult : IOperationResult<ClinicRequestValidation>
    {
        public ClinicRequestValidation Result { get; }
        public IReadOnlyList<IOperationFailure> Failures { get; }
        public bool Succeed { get; }

        public OperationClinicResult(ClinicRequestValidation result)
        {
            Result = result;
            Succeed = true;
        }
        public OperationClinicResult(IReadOnlyList<IOperationFailure> failures)
        {
            Failures = failures;
        }
    }
}