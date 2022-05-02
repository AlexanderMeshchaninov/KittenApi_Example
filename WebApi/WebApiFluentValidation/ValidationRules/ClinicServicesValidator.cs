using FluentValidation;
using WebApiFluentValidation.Interfaces;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.Services;

namespace WebApiFluentValidation.ValidationRules
{
    public interface IClinicServicesValidator : IFluentValidationService<ClinicServiceRequestValidation>
    {
    }

    public sealed class ClinicServicesValidator : 
        FluentValidationService<ClinicServiceRequestValidation>, 
        IClinicServicesValidator
    {
        public ClinicServicesValidator()
        {
            RuleFor(x => x.KittenId)
                .NotEmpty()
                .WithMessage("Id животного не может быть пустым")
                .WithErrorCode("BRL-103.1");
            RuleFor(x => x.MedicalProcedure)
                .NotEmpty()
                .WithMessage("Медицинская процедура не может быть пустой")
                .WithErrorCode("BRL-103.2");
        }
    }
}