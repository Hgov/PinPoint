using PinPoint.Core.FluentValidation;
using PinPoint.Core.LoggerManager;
using PinPoint.Core.Repositories;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.Infrastructure.LoggerService;
using PinPoint.Infrastructure.Repositories;

namespace PinPoint.Infrastructure.UnitOfWork.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _DataContext;

        public UnitOfWork(DataContext DataContext)
        {
            loggerManager = new LoggerManager();
            _DataContext = DataContext;
            contactRepository = new ContactRepository(_DataContext);
            fluentValidationContact = new FluentValidation.AbstractValidators.ContactValidator(contactRepository);

        }
        public IContactRepository contactRepository { get; private set; }

        public ILoggerManager loggerManager { get; private set; }

        public IFluentValidation<Contact> fluentValidationContact { get; private set; }

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
