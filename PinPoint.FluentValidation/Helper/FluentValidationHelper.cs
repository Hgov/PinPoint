using Microsoft.AspNetCore.Components.Forms;
using PinPoint.Core.UnitOfWork.Base;
using System.Text.RegularExpressions;
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

        public bool IsPasswordValid(string arg)
        {
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            return regex.IsMatch(arg);
        }

        public bool IsEmailExist(string Email)
        {
            return !_uow.userRepository.IsEmailExist(Email).Result;
        }
        public bool IsUserByIdExist(Guid? id)
        {
            return _uow.userRepository.IsUserByIdExist((Guid)id).Result;
        }
        public bool IsUserAllExist(Guid? id)
        {
            return _uow.userRepository.GetAllAsync().Result.Count() > 0 ? true : false;
        }
    }
}
