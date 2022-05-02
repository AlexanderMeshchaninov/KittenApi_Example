using System.Threading.Tasks;
using WebApiFluentValidation.Interfaces;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.ValidationRules;

namespace WebApiFluentValidation.Services
{
    public interface IOperationClinicService : IOperationResultService<ClinicRequestValidation>
    {
    }

    public sealed class OperationClinicService : IOperationClinicService
    {
        private readonly IClinicValidator _validator;

        public OperationClinicService(IClinicValidator validator)
        {
            _validator = validator;
        }

        public async Task<IOperationResult<ClinicRequestValidation>> StartValidationAsync(ClinicRequestValidation request)
        {
            var result = await _validator.ValidateEntityAsync(request);
            
            if (result is null || result.Count == 0)
            {
                return new OperationClinicResult(new ClinicRequestValidation());
            }

            return new OperationClinicResult(result);
        }
    }
}