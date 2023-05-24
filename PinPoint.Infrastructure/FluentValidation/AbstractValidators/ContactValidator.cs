using FluentValidation;
using PinPoint.Core.FluentValidation;
using PinPoint.Core.Repositories;
using PinPoint.Data.Domain;
using System.Text.RegularExpressions;
using static PinPoint.Data.Enums.Enums;

namespace PinPoint.Infrastructure.FluentValidation.AbstractValidators
{
    public class ContactValidator : AbstractValidator<Contact>, IFluentValidation<Contact>
    {
        private readonly IContactRepository _contactRepository;
        public ContactValidator(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public AbstractValidator<Contact> GetValidationRules()
        {
            return this;
        }
        public AbstractValidator<Contact> GetValidationByIdRules()
        {
            return this;
        }
        public AbstractValidator<Contact> PostValidationRules()
        {
            RuleFor(c => c.first_name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.last_name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.email).EmailAddress().WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.email)).Must(IsEmailExist).WithMessage("{PropertyName} Is Already Exist.").When(i => !string.IsNullOrEmpty(i.email));
            RuleFor(p => p.phone).MinimumLength(10).WithMessage("{PropertyName} must not be less than 10 characters.").When(i => !string.IsNullOrEmpty(i.phone)).MaximumLength(20).WithMessage("{PropertyName} must not exceed 50 characters.").When(i => !string.IsNullOrEmpty(i.phone)).Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.phone));
            RuleFor(c => c.gender).Must(GenderBeAValidParameter).WithMessage("{PropertyName} not valid");
            RuleFor(c => c.birth_date).Must(BeAValidDate).WithMessage("{PropertyName} not valid");
            return this;
        }
        public AbstractValidator<Contact> PutValidationRules()
        {
            RuleFor(c => c.first_name)
                .NotEmpty().WithMessage("{PropertyName} is required.").When(i => !string.IsNullOrEmpty(i.first_name));

            RuleFor(c => c.last_name)
                .NotEmpty().WithMessage("{PropertyName} is required.").When(i => !string.IsNullOrEmpty(i.last_name));

            RuleFor(c => c.email)
                .EmailAddress().WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.email));

            RuleFor(p => p.phone)
                .MinimumLength(10).WithMessage("{PropertyName} must not be less than 10 characters.").When(i => !string.IsNullOrEmpty(i.phone))
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed 50 characters.").When(i => !string.IsNullOrEmpty(i.phone))
                .Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")).WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.phone));

            RuleFor(c => c.gender).Must(GenderBeAValidParameter).When(i => i.gender != null).WithMessage("{PropertyName} not valid");

            RuleFor(c => c.birth_date)
                .Must(BeAValidDate).WithMessage("{PropertyName} not valid").When(i => !string.IsNullOrEmpty(i.birth_date.ToString()));


            return this;
        }

        public async Task<Contact> PutCompareRulesAsync(Guid id, Contact newData)
        {
            Contact _data = await _contactRepository.GetByIDAsync(id);
            if (_data == null)
                return null;
            else
            {
                _data.contact_id = id;
                _data.first_name = (!string.IsNullOrWhiteSpace(newData.first_name) && _data.first_name != newData.first_name) ? newData.first_name : _data.first_name;
                _data.last_name = (!string.IsNullOrWhiteSpace(newData.last_name) && _data.last_name != newData.last_name) ? newData.last_name : _data.last_name;
                _data.email = (!string.IsNullOrWhiteSpace(newData.email) && _data.email != newData.email) ? newData.email : _data.email;
                _data.phone = (!string.IsNullOrWhiteSpace(newData.phone) && _data.phone != newData.phone) ? newData.phone : _data.phone;
                _data.birth_date = (newData.birth_date.HasValue && _data.birth_date != newData.birth_date) ? newData.birth_date : _data.birth_date;
                _data.bio = (!string.IsNullOrWhiteSpace(newData.bio) && _data.bio != newData.bio) ? newData.bio : _data.bio;
                _data.gender = (_data.gender != newData.gender && newData.gender != null) ? newData.gender : _data.gender;
                _data.last_updated_tsz = (newData.last_updated_tsz != null && _data.last_updated_tsz != newData.last_updated_tsz) ? newData.last_updated_tsz : _data.last_updated_tsz;
                _data.status_active = (_data.status_active != newData.status_active) ? newData.status_active : _data.status_active;
                _data.status_visibility = (_data.status_visibility != newData.status_visibility) ? newData.status_visibility : _data.status_visibility;

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
            return !_contactRepository.IsEmailExist(Email).Result;
        }
    }
}
