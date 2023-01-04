using FluentValidation;
using PinPoint.Core.FluentValidation;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.FluentValidation.Helper;
using PinPoint.Infrastructure.UnitOfWork.Base;
using System.Text.RegularExpressions;

namespace PinPoint.FluentValidation.AbstractValidation
{
    public class UserValidator : AbstractValidator<User>, IFluentValidation<User>
    {
        private readonly FluentValidationHelper _fluentValidationHelper;
        private readonly IUnitOfWork _uow;
        private readonly DataContext _dataContext;
        public UserValidator(DataContext dataContext)
        {
            _dataContext = dataContext;
            _uow = new UnitOfWork(_dataContext);
            _fluentValidationHelper = new FluentValidationHelper(_uow);
        }
        public AbstractValidator<User> GetRules() {
            return this;
        }
        public AbstractValidator<User> GetByIdRules()
        {
            RuleFor(c => c.user_id).Must(_fluentValidationHelper.IsUserByIdExist).WithMessage("{PropertyName} No records found.");
            return this;
        }
        public AbstractValidator<User> PostRules()
        {
            RuleFor(c => c.first_name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.last_name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.email).EmailAddress().WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.email)).Must(_fluentValidationHelper.IsEmailExist).WithMessage("{PropertyName} Is Already Exist.").When(i => !string.IsNullOrEmpty(i.email));
            RuleFor(p => p.phone).MinimumLength(10).WithMessage("{PropertyName} must not be less than 10 characters.").When(i => !string.IsNullOrEmpty(i.phone)).MaximumLength(20).WithMessage("{PropertyName} must not exceed 50 characters.").When(i => !string.IsNullOrEmpty(i.phone)).Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.phone));
            RuleFor(c => c.gender).Must(_fluentValidationHelper.GenderBeAValidParameter).WithMessage("{PropertyName} not valid");
            RuleFor(c => c.birth_date).Must(_fluentValidationHelper.BeAValidDate).WithMessage("{PropertyName} not valid");
            return this;
        }

        public AbstractValidator<User> PutRules()
        {
            RuleFor(c => c.first_name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.last_name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.email).EmailAddress().WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.email)).Must(_fluentValidationHelper.IsEmailExist).WithMessage("{PropertyName} Is Already Exist.").When(i => !string.IsNullOrEmpty(i.email));
            RuleFor(p => p.phone).MinimumLength(10).WithMessage("{PropertyName} must not be less than 10 characters.").When(i => !string.IsNullOrEmpty(i.phone)).MaximumLength(20).WithMessage("{PropertyName} must not exceed 50 characters.").When(i => !string.IsNullOrEmpty(i.phone)).Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.phone));
            RuleFor(c => c.gender).Must(_fluentValidationHelper.GenderBeAValidParameter).WithMessage("{PropertyName} not valid");
            RuleFor(c => c.birth_date).Must(_fluentValidationHelper.BeAValidDate).WithMessage("{PropertyName} not valid");
            return this;
        }

    }
}
