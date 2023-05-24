using FluentValidation;

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
