using AutoMapper;
using PinkPoint.Core.LoggerManager;
using PinkPoint.Infrastructure.LoggerService;

namespace PinkPoint.API
{
    public static class Extensions
    {
        public static IServiceCollection ConfigureLoggerService(this IServiceCollection services)
            => services.AddSingleton<ILoggerManager, LoggerManager>();

    }
}
