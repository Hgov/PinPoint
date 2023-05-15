using AutoMapper;
using PinPoint.Core.FluentValidation;
using PinPoint.Core.LoggerManager;
using PinPoint.Core.Repositories;
using PinPoint.Data.Domain;

namespace PinPoint.Core.UnitOfWork.Base
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Manager & Controls
        /// </summary>
        ILoggerManager loggerManager { get; }
        IFluentValidation<Contact> fluentValidationContact { get; }

        /// <summary>
        /// Data & Repositories
        /// </summary>
        IContactRepository contactRepository { get; }


        /// <summary>
        /// Mission
        /// </summary>
        /// <returns></returns>
        int Complete();
        void Dispose();
    }
}
