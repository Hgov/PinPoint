using FluentValidation;
using PinPoint.Application.Interface;
using PinPoint.Core.FluentValidation;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.FluentValidation.AbstractValidation;
using PinPoint.Infrastructure.Base;
using PinPoint.Infrastructure.UnitOfWork.Base;

namespace PinPoint.Application.Service
{
    public class UserService : PinPointDataAccessHelper, IUserService<User>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFluentValidationUser<User> _userValidator;

        public UserService(DataContext dataContext) : base(dataContext)
        {
            _uow = new Infrastructure.UnitOfWork.Base.UnitOfWork(_dataContext);
            _userValidator = new UserValidator(_dataContext);
        }

        public async Task<IEnumerable<User>> GetUserListAsync()
        {
            var _user = await _uow.userRepository.GetAllAsync();
            return _user;
        }
        public async Task<User> GetByIdUserAsync(Guid id)
        {
            var _user = await _uow.userRepository.GetByIDAsync(id);
            return _user;
        }
        public async Task<User> PostUserAsync(User user)
        {
            _userValidator.PostRules().Validate(user, options =>
            {
                options.ThrowOnFailures();
            });
            var _user = await _uow.userRepository.AddAsync(user);
            _uow.Complete();
            return _user;
        }

    }
}
