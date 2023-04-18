using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PinPoint.Core.LoggerManager;
using PinPoint.Core.UnitOfWork.Base;
using PinPoint.DataAccess.Helpers;

namespace PinPoint.Infrastructure.BaseClass
{
    public class BaseService : ControllerBase
    {
        protected readonly DataContext _dataContext;
        protected readonly IUnitOfWork _uow;
        protected readonly IMapper _mapper;
        protected readonly ILoggerManager _logger;
        public BaseService(DataContext dataContext, IMapper mapper, ILoggerManager logger)
        {
            _logger = logger;
            _logger?.LogDebug("BaseClass()");
            _dataContext = dataContext;
            _uow = new UnitOfWork.Base.UnitOfWork(_dataContext);
            _mapper = mapper;
        }

    }
}
