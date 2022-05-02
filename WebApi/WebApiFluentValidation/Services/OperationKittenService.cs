using System.Threading.Tasks;
using WebApiFluentValidation.Interfaces;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.ValidationRules;

namespace WebApiFluentValidation.Services
{
    public interface IOperationKittenService : IOperationResultService<KittenRequestValidation>
    {
    }

    public sealed class OperationKittenService : IOperationKittenService
    {
        private readonly IKittenValidator _validator;

        public OperationKittenService(IKittenValidator validator)
        {
            _validator = validator;
        }

        public async Task<IOperationResult<KittenRequestValidation>> StartValidationAsync(KittenRequestValidation request)
        {
            var result = await _validator.ValidateEntityAsync(request);
            
            if (result is null || result.Count == 0)
            {
                return new OperationKittenResult(new KittenRequestValidation());
            }

            return new OperationKittenResult(result);
        }
    }
}