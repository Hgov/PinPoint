using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PinkPoint.Data.Enums.Enums;

namespace PinkPoint.FluentValidation.Helper
{
    public class FluentValidationHelper
    {
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
