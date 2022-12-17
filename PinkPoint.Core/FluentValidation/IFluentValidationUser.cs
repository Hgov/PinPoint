using FluentValidation;

namespace PinkPoint.Core.FluentValidation
{
    public interface IFluentValidationUser<User> where User : class
    {
        AbstractValidator<User> PostRules();
    }
}
