using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using PinPoint.Core.LoggerManager;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.DataAccess.Helpers;
namespace PinPoint.Infrastructure.BaseClass
{
    public class BaseController<T> : Controller where T : class, new()
    {
        protected readonly DataContext _dataContext;
        protected readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;
        protected readonly ILoggerManager _logger;

        public BaseController(DataContext dataContext, IMapper mapper, ILoggerManager logger)
        {
            _logger = logger;
            _dataContext = dataContext;
            _uow = new UnitOfWork.Base.UnitOfWork(_dataContext);
            _mapper = mapper;
            _logger?.LogDebug($"BaseController<{typeof(T)}>()");
        }
    }
}
