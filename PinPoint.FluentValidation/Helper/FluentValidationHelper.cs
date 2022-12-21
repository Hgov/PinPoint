using PinPoint.Core.UnitOfWork.Base;
using static PinPoint.Data.Enums.Enums;

namespace PinPoint.FluentValidation.Helper
{
    public class FluentValidationHelper
    {
        public IUnitOfWork _uow;
        public FluentValidationHelper(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public bool GenderBeAValidParameter(Gender? arg)
        {
            return arg.Equals(Gender.Male) || arg.Equals(Gender.Null) || arg.Equals(Gender.Female);
        }

        public bool BoolDefaultAValidParameter(bool? arg)
        {
            return arg.Equals(false);
        }

        public bool BeAValidDate(DateTime? date)
        {
            return !date.Equals(default(DateTime));
        }

        public bool BeAValidBool(bool? boolean)
        {
            return !boolean.Equals(default(bool));
        }
    }
}
