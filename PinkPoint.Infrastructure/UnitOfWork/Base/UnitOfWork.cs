using PinPoint.Core.Repositories;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.DataAccess.Helpers;
using PinPoint.Infrastructure.Repositories;

namespace PinPoint.Infrastructure.UnitOfWork.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _DataContext;

        public UnitOfWork(DataContext DataContext)
        {
            _DataContext = DataContext;
            userRepository = new UserRepository(_DataContext);
        }
        public IUserRepository userRepository { get; private set; }

        public int Complete()
        {

            return _DataContext.SaveChanges();
        }

        public void Dispose()
        {
            _DataContext.Dispose();
        }
    }
}
