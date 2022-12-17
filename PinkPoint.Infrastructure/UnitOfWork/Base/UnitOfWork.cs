using PinkPoint.Core.Repositories;
using PinkPoint.Core.UnitOfWork.Base;
using PinkPoint.DataAccess.Helpers;
using PinkPoint.Infrastructure.Repositories;

namespace PinkPoint.Infrastructure.UnitOfWork.Base
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
