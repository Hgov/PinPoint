
using PinPoint.API.Middleware;
using PinPoint.Core.LoggerManager;
using PinPoint.Infrastructure.LoggerService;
using PinPoint.Infrastructure.MapperService.Profiles;

namespace PinPoint.API
{
    public static class Extensions
    {
        public static IServiceCollection ConfigureLoggerService(this IServiceCollection services)
            => services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
        }
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapperMiddleware>();
        }
    }


}
