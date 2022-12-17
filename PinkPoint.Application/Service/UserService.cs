using FluentValidation;
using PinkPoint.Application.Interface;
using PinkPoint.Core.FluentValidation;
using PinkPoint.Core.UnitOfWork.Base;
using PinkPoint.Data.Domain;
using PinkPoint.DataAccess.Helpers;
using PinkPoint.FluentValidation.AbstractValidation;
using PinkPoint.Infrastructure.Base;
using PinkPoint.Infrastructure.UnitOfWork.Base;

namespace PinkPoint.Application.Service
{
    public class UserService : PinPointDataAccessHelper, IUserService<User>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFluentValidationUser<User> _userValidator;

        public UserService(DataContext dataContext) : base(dataContext)
        {
            _uow = new UnitOfWork(_dataContext);
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
