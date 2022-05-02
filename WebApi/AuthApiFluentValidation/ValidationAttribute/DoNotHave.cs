using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuthApiFluentValidation.Models;

namespace AuthApiFluentValidation.ValidationAttribute
{
    public sealed class DoNotHave : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        private string[] _words;

        public DoNotHave(string[] words)
        {
            _words = words;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var bad = new HashSet<string>(_words, StringComparer.InvariantCultureIgnoreCase);
            var request = (UserRequestValidation) validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(request.UserName))
            {
                return ValidationResult.Success;
            }

            var words = new HashSet<string>(request.UserName.Split(""), StringComparer.InvariantCultureIgnoreCase);

            foreach (var word in words)
            {
                if (bad.Contains(word))
                {
                    return new ValidationResult("Имя содержит недопустимые символы", new[] {nameof(request.UserName)});
                }
            }

            return ValidationResult.Success;
        }
    }
}