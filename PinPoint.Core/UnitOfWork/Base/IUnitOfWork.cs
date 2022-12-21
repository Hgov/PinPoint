using PinPoint.Core.LoggerManager;
using PinPoint.Core.Repositories;

namespace PinPoint.Core.UnitOfWork.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository userRepository { get; }
        int Complete();
    }
}
