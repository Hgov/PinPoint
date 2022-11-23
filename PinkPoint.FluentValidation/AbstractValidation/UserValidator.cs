using FluentValidation;
using PinkPoint.Data.Domain;
namespace PinkPoint.FluentValidation.AbstractValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.first_name).NotEmpty().WithMessage("Ad Alanı Boş Olamaz.");
        }
    }
}
