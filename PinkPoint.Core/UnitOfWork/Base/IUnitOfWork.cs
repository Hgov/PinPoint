using PinkPoint.Core.Repositories;

namespace PinkPoint.Core.UnitOfWork.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository userRepository { get; }
        int Complete();
    }
}
