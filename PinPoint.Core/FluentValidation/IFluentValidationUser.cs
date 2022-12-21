using FluentValidation;

namespace PinPoint.Core.FluentValidation
{
    public interface IFluentValidationUser<User> where User : class
    {
        AbstractValidator<User> PostRules();
    }
}
