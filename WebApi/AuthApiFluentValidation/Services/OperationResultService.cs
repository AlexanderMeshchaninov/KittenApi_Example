using System.Threading.Tasks;
using AuthApiFluentValidation.Models;
using AuthApiFluentValidation.ValidationRules;
using AuthApiFluentValidationAbstraction.Interfaces;

namespace AuthApiFluentValidation.Services
{
    public interface IOperationService : IOperationResultService<UserRequestValidation>
    {
    }

    public class OperationResultService : IOperationService
    {
        private readonly IUserValidator _validator;
        
        public OperationResultService(IUserValidator validator)
        {
            _validator = validator;
        }

        public async Task<IOperationResult<UserRequestValidation>> StartValidationAsync(UserRequestValidation request)
        {
            var result = await _validator.ValidateEntityAsync(request);
            
            if (result is null || result.Count == 0)
            {
                return new OperationResult(new UserRequestValidation());
            }

            return new OperationResult(result);
        }
    }
}