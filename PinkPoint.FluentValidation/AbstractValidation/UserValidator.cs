using FluentValidation;
using PinkPoint.Core.FluentValidation;
using PinkPoint.Core.UnitOfWork.Base;
using PinkPoint.Data.Domain;
using PinkPoint.DataAccess.Helpers;
using PinkPoint.FluentValidation.Helper;
using PinkPoint.Infrastructure.UnitOfWork.Base;
using System.Text.RegularExpressions;

namespace PinkPoint.FluentValidation.AbstractValidation
{
    public class UserValidator : AbstractValidator<User>, IFluentValidationUser<User>
    {
        public FluentValidationHelper _fluentValidationHelper;
        public IUnitOfWork _uow;
        private readonly DataContext _dataContext;
        public UserValidator(DataContext dataContext)
        {
            _dataContext = dataContext;
            _uow = new UnitOfWork(_dataContext);
            _fluentValidationHelper = new FluentValidationHelper(_uow);
        }
        public AbstractValidator<User> PostRules()
        {
            RuleFor(c => c.first_name).NotEmpty().WithMessage("Firstname is required.");
            RuleFor(c => c.last_name).NotEmpty().WithMessage("Lastname is required.");
            RuleFor(c => c.email).EmailAddress().WithMessage("Email not valid").When(i => !string.IsNullOrEmpty(i.email));
            RuleFor(p => p.phone).MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.").MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.").Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("PhoneNumber not valid");
            RuleFor(c => c.gender).Must(_fluentValidationHelper.GenderBeAValidParameter).WithMessage("Gender not valid");
            RuleFor(c => c.birth_date).Must(_fluentValidationHelper.BeAValidDate).WithMessage("Birth Date not valid");
            return this;
        }

    }
}
