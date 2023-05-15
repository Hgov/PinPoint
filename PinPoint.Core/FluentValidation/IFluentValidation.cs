using FluentValidation;
using PinPoint.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPoint.Core.FluentValidation
{
    public interface IFluentValidation<TEntity> where TEntity : class
    {
        AbstractValidator<TEntity> GetValidationRules();
        AbstractValidator<TEntity> GetValidationByIdRules();
        AbstractValidator<TEntity> PostValidationRules();
        AbstractValidator<TEntity> PutValidationRules();
        Task<TEntity> PutCompareRulesAsync(Guid id, TEntity newData);
    }
}
