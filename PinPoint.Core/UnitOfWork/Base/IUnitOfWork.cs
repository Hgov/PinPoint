using AutoMapper;
using PinPoint.Core.FluentValidation;
using PinPoint.Core.LoggerManager;
using PinPoint.Core.Repositories;
using PinPoint.Data.Domain;

namespace PinPoint.Core.UnitOfWork.Base
{
    public interface IUnitOfWork : IDisposable
    {
        ILoggerManager loggerManager { get; }
        IUserRepository userRepository { get; }
        IFluentValidation<User> fluentValidationUser { get; }
        int Complete();
    }
}
