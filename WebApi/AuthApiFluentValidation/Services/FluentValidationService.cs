using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthApiFluentValidation.Models;
using AuthApiFluentValidationAbstraction.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace AuthApiFluentValidation.Services
{
    public abstract class FluentValidationService<TEntity> : 
        AbstractValidator<TEntity>, 
        IFluentValidationService<TEntity> 
        where TEntity : class
    {
        public async Task<IReadOnlyList<IOperationFailure>> ValidateEntityAsync(TEntity item)
        {
            ValidationResult result = await ValidateAsync(item);

            if (result is null || result.Errors.Count == 0)
            {
                return ArraySegment<IOperationFailure>.Empty;
            }

            List<IOperationFailure> failures = new List<IOperationFailure>(result.Errors.Count);

            foreach (ValidationFailure error in result.Errors)
            {
                OperationFailure failure = new OperationFailure(error.PropertyName, error.ErrorMessage, error.ErrorCode);

                failures.Add(failure);
            }

            return failures;
        }
    }
}