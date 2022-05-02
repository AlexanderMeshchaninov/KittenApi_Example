using System.Threading.Tasks;
using WebApiFluentValidation.Interfaces;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.ValidationRules;

namespace WebApiFluentValidation.Services
{
    public interface IOperationClinicMedService : IOperationResultService<ClinicServiceRequestValidation>
    {
    }

    public sealed class OperationClinicMedService : IOperationClinicMedService
    {
        private readonly IClinicServicesValidator _validator;

        public OperationClinicMedService(IClinicServicesValidator validator)
        {
            _validator = validator;
        }

        public async Task<IOperationResult<ClinicServiceRequestValidation>> StartValidationAsync(ClinicServiceRequestValidation request)
        {
            var result = await _validator.ValidateEntityAsync(request);
            
            if (result is null || result.Count == 0)
            {
                return new OperationClinicServiceResult(new ClinicServiceRequestValidation());
            }

            return new OperationClinicServiceResult(result);
        }
    }
}