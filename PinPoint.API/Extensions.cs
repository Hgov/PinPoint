﻿
using PinPoint.API.Middleware;
using PinPoint.Application.Interface;
using PinPoint.Application.Service;
using PinPoint.Core.LoggerManager;
using PinPoint.Data.Domain;
using PinPoint.Infrastructure.LoggerService;
using PinPoint.Infrastructure.MapperService.Profiles;

namespace PinPoint.API
{
    public static class Extensions
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.AddScoped<IContactService<Contact>, ContactService>();
        }

        public static void ConfigureMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ContactProfile));
        }
        public static IApplicationBuilder UseResponseWrapperMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapperMiddleware>();
        }
    }


}
