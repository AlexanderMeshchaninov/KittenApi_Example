using AuthApiFluentValidation.Models;
using AuthApiFluentValidation.Services;
using AuthApiFluentValidation.ValidationRules;
using AuthApiFluentValidationAbstraction.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AuthApiFluentValidation.Registration
{
    public static class AuthApiFluentValidationRegister
    {
        public static IServiceCollection RegisterAuthFluentValidator(this IServiceCollection services)
        {
            return services
                .AddTransient<IOperationService, OperationResultService>()
                .AddTransient<IUserRequestValidation, UserRequestValidation>()
                .AddTransient<IUserValidator, UserValidator>();
        }
    }
}