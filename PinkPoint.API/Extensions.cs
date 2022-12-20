using AutoMapper;
using PinPoint.Core.LoggerManager;
using PinPoint.Infrastructure.LoggerService;

namespace PinPoint.API
{
    public static class Extensions
    {
        public static IServiceCollection ConfigureLoggerService(this IServiceCollection services)
            => services.AddSingleton<ILoggerManager, LoggerManager>();

    }
}
