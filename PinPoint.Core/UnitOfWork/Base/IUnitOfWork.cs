using AutoMapper;
using PinPoint.Core.FluentValidation;
using PinPoint.Core.LoggerManager;
using PinPoint.Core.Repositories;
using PinPoint.Data.Domain;

namespace PinPoint.Core.UnitOfWork.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository userRepository { get; }
        ILoggerManager loggerManager { get; }
        int Complete();
    }
}
