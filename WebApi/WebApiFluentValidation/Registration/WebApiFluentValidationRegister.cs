using WebApiFluentValidation.Interfaces;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.Services;
using Microsoft.Extensions.DependencyInjection;
using WebApiFluentValidation.ValidationRules;

namespace WebApiFluentValidation.Registration
{
    public static class WebApiFluentValidationRegister
    {
        public static IServiceCollection RegisterWebApiFluentValidator(this IServiceCollection services)
        {
            return services
                .AddTransient<IClinicValidator, ClinicValidator>()
                .AddTransient<IKittenValidator, KittenValidator>()
                .AddTransient<IClinicServicesValidator, ClinicServicesValidator>()

                .AddTransient<IOperationClinicService, OperationClinicService>()
                .AddTransient<IOperationKittenService, OperationKittenService>()
                .AddTransient<IOperationClinicMedService, OperationClinicMedService>()

                .AddTransient<IClinicRequestValidation, ClinicRequestValidation>()
                .AddTransient<IKittenRequestValidation, KittenRequestValidation>()
                .AddTransient<IClinicServiceRequestValidation, ClinicServiceRequestValidation>();
        }
    }
}