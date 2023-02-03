using FluentValidation;
using PinPoint.Core.FluentValidation;
using PinPoint.Core.Repositories;
using PinPoint.Data.Domain;
using System.Text.RegularExpressions;
using static PinPoint.Data.Enums.Enums;

namespace PinPoint.Infrastructure.FluentValidation.AbstractValidators
{
    public class UserValidator : AbstractValidator<User>, IFluentValidation<User>
    {
        private readonly IUserRepository _userRepository;
        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public AbstractValidator<User> GetValidationRules()
        {
            return this;
        }
        public AbstractValidator<User> GetValidationByIdRules()
        {
            return this;
        }
        public AbstractValidator<User> PostValidationRules()
        {
            RuleFor(c => c.first_name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.last_name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.email).EmailAddress().WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.email)).Must(IsEmailExist).WithMessage("{PropertyName} Is Already Exist.").When(i => !string.IsNullOrEmpty(i.email));
            RuleFor(p => p.phone).MinimumLength(10).WithMessage("{PropertyName} must not be less than 10 characters.").When(i => !string.IsNullOrEmpty(i.phone)).MaximumLength(20).WithMessage("{PropertyName} must not exceed 50 characters.").When(i => !string.IsNullOrEmpty(i.phone)).Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.phone));
            RuleFor(c => c.gender).Must(GenderBeAValidParameter).WithMessage("{PropertyName} not valid");
            RuleFor(c => c.birth_date).Must(BeAValidDate).WithMessage("{PropertyName} not valid");
            return this;
        }
        public AbstractValidator<User> PutValidationRules()
        {
            RuleFor(c => c.first_name)
                .NotEmpty().WithMessage("{PropertyName} is required.").When(i => !string.IsNullOrEmpty(i.first_name));

            RuleFor(c => c.last_name)
                .NotEmpty().WithMessage("{PropertyName} is required.").When(i => !string.IsNullOrEmpty(i.last_name));

            RuleFor(c => c.email)
                .EmailAddress().WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.email))
                .Must(IsEmailExist).WithMessage("{PropertyName} Is Already Exist.").When(i => !string.IsNullOrEmpty(i.email));

            RuleFor(p => p.phone)
                .MinimumLength(10).WithMessage("{PropertyName} must not be less than 10 characters.").When(i => !string.IsNullOrEmpty(i.phone))
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed 50 characters.").When(i => !string.IsNullOrEmpty(i.phone))
                .Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.phone));

            RuleFor(c => c.gender)
                .Must(GenderBeAValidParameter).WithMessage("{PropertyName} not valid");

            RuleFor(c => c.birth_date)
                .Must(BeAValidDate).WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.birth_date.ToString()));


            return this;
        }

        public async Task<User> PutCompareRulesAsync(Guid id, User newUserData)
        {
            User _data = await _userRepository.GetByIDAsync(id);
            if (_data == null)
                return null;
            else
            {
                _data.user_id = id;
                _data.first_name = (!string.IsNullOrWhiteSpace(newUserData.first_name) && _data.first_name != newUserData.first_name) ? newUserData.first_name : _data.first_name;
                _data.last_name = (!string.IsNullOrWhiteSpace(newUserData.last_name) && _data.last_name != newUserData.last_name) ? newUserData.last_name : _data.last_name;
                _data.email = (!string.IsNullOrWhiteSpace(newUserData.email) && _data.email != newUserData.email) ? newUserData.email : _data.email;
                _data.phone = (!string.IsNullOrWhiteSpace(newUserData.phone) && _data.phone != newUserData.phone) ? newUserData.phone : _data.phone;
                _data.birth_date = (newUserData.birth_date != null && _data.birth_date != newUserData.birth_date) ? newUserData.birth_date : _data.birth_date;
                _data.bio = (!string.IsNullOrWhiteSpace(newUserData.bio) && _data.bio != newUserData.bio) ? newUserData.bio : _data.bio;
                _data.gender = (_data.gender != newUserData.gender) ? newUserData.gender : _data.gender;
                _data.last_updated_tsz = (newUserData.last_updated_tsz != null && _data.last_updated_tsz != newUserData.last_updated_tsz) ? newUserData.last_updated_tsz : _data.last_updated_tsz;
                _data.status_active = (_data.status_active != newUserData.status_active) ? newUserData.status_active : _data.status_active;
                _data.status_visibility = (_data.status_visibility != newUserData.status_visibility) ? newUserData.status_visibility : _data.status_visibility;

                return _data;
            }
        }


        public bool GenderBeAValidParameter(Gender? arg)
        {
            return arg.Equals(Gender.Male) || arg.Equals(Gender.Null) || arg.Equals(Gender.Female);
        }

        public bool BeAValidDate(DateTime? date)
        {
            return !date.Equals(default(DateTime));
        }

        public bool IsEmailExist(string Email)
        {
            return !_userRepository.IsEmailExist(Email).Result;
        }
    }
}
