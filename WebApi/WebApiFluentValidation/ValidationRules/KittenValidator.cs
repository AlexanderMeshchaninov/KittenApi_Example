using FluentValidation;
using WebApiFluentValidation.Interfaces;
using WebApiFluentValidation.Models;
using WebApiFluentValidation.Services;

namespace WebApiFluentValidation.ValidationRules
{
    public interface IKittenValidator : IFluentValidationService<KittenRequestValidation>
    {
    }

    public sealed class KittenValidator : 
        FluentValidationService<KittenRequestValidation>, 
        IKittenValidator
    {
        public KittenValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id животного не может быть пустым")
                .WithErrorCode("BRL-102.1");
            RuleFor(x => x.NickName)
                .NotEmpty()
                .WithMessage("Имя животного не может быть пустым")
                .WithErrorCode("BRL-102.2");
            RuleFor(x => x.NickName)
                .MinimumLength(2)
                .WithMessage("Имя животного не должно быть меньше 1 буквы")
                .WithErrorCode("BRL-102.3");
            RuleFor(x => x.NickName)
                .MaximumLength(10)
                .WithMessage("Имя животного не должно превышать 10 букв")
                .WithErrorCode("BRL-102.4");
            RuleFor(x => x.Color)
                .NotEmpty()
                .WithMessage("Окрас животного не может быть пустым")
                .WithErrorCode("BRL-102.5");
            RuleFor(x => x.Weight)
                .NotEmpty()
                .WithMessage("Вес животного не может быть пустым")
                .WithErrorCode("BRL-102.6");
            RuleFor(x => x.Weight)
                .Must(x => !x.Equals(x <= 0))
                .WithMessage("Вес животного не может меньше нуля")
                .WithErrorCode("BRL-102.7");
            RuleFor(x => x.Page)
                .NotEmpty()
                .WithMessage("Страница не может быть пустой")
                .WithErrorCode("BRL-101.8");
            RuleFor(x => x.Size)
                .NotEmpty()
                .WithMessage("Размер списка не может быть пустым")
                .WithErrorCode("BRL-101.9");
        }
    }
}