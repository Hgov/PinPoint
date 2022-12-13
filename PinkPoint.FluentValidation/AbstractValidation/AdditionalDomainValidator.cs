using FluentValidation;
using PinkPoint.Data.Domain.Base;
using PinkPoint.FluentValidation.Helper;
using System.Text.RegularExpressions;

namespace PinkPoint.FluentValidation.AbstractValidation
{
    public class AdditionalDomainValidator : AbstractValidator<AdditionalDomain>
    {
        public AdditionalDomainValidator()
        {

            //FluentValidationHelper _fluentValidationHelper = new FluentValidationHelper();
            //RuleFor(c => c.creation_tsz).Must(_fluentValidationHelper.BeAValidDate).WithMessage("creation_tsz Date not valid");
            //RuleFor(c => c.last_updated_tsz).Must(_fluentValidationHelper.BeAValidDate).WithMessage("last_updated_tsz Date not valid");
            //RuleFor(c => c.delete_tsz).Must(_fluentValidationHelper.BeAValidDate).WithMessage("delete_tsz Date not valid");
            //RuleFor(p => p.status_visibility).Must(_fluentValidationHelper.BoolDefaultAValidParameter).WithMessage("status_visibility not valid");
            //RuleFor(c => c.status_active).Must(_fluentValidationHelper.BoolDefaultAValidParameter).WithMessage("status_active not valid");
        }
    }
}
