using FluentValidation;
using WebApiFluentValidation.Interfaces;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.Services;

namespace WebApiFluentValidation.ValidationRules
{
    public interface IClinicValidator : IFluentValidationService<ClinicRequestValidation>
    {
    }

    public sealed class ClinicValidator : 
        FluentValidationService<ClinicRequestValidation>, 
        IClinicValidator
    {
        public ClinicValidator()
        {
            RuleFor(x => x.ClinicName)
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым")
                .WithErrorCode("BRL-101.1");
            RuleFor(x => x.ClinicName)
                .MinimumLength(3)
                .WithMessage("Имя не должно содержать меньше 3 букв")
                .WithErrorCode("BRL-101.2");
            RuleFor(x => x.ClinicName)
                .MaximumLength(10)
                .WithMessage("Имя не должно содержать более 10 букв")
                .WithErrorCode("BRL-101.3");
            RuleFor(x => x.ClinicId)
                .NotEmpty()
                .WithMessage("Id клиники или животного не могут быть пустыми")
                .WithErrorCode("BRL-101.4");
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id клиники или животного не могут быть пустыми")
                .WithErrorCode("BRL-101.4");
            RuleFor(x => x.KittenId)
                .NotEmpty()
                .WithMessage("Id клиники или животного не могут быть пустыми")
                .WithErrorCode("BRL-101.4");
            RuleFor(x => x.Page)
                .NotEmpty()
                .WithMessage("Страница не может быть пустой")
                .WithErrorCode("BRL-101.5");
            RuleFor(x => x.Size)
                .NotEmpty()
                .WithMessage("Размер списка не может быть пустым")
                .WithErrorCode("BRL-101.6");
        }
    }
}