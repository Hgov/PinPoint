﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PinPoint.Infrastructure.LoggerService;
using System.Net;
using PinPoint.Infrastructure.ResponseWrapper;

namespace PinPoint.API.Middleware
{
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LoggerManager loggerManager;
        public ResponseWrapperMiddleware(RequestDelegate next)
        {
            loggerManager = new LoggerManager();
            _next = next;
        }
        public class ResponseWrapperData
        {
            public object value { get; set; }
            public int statusCode { get; set; }
        }
        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                //set the current response to the memorystream.
                context.Response.Body = memoryStream;

                await _next(context);

                //reset the body 
                context.Response.Body = currentBody;
                memoryStream.Seek(0, SeekOrigin.Begin);

                string? readToEnd = new StreamReader(memoryStream).ReadToEnd();
                JObject readToEndSingle = JObject.Parse(readToEnd);
                //int? statusCode = (int?)readToEndSingle["statusCode"];
                object objResult = JsonConvert.DeserializeObject<ResponseWrapperData>(readToEndSingle.ToString());
                ResponseWrapperManager? result = null;
                if ((int)(HttpStatusCode)context.Response.StatusCode >= 100 && (int)(HttpStatusCode)context.Response.StatusCode < 400)
                {
                    result = ResponseWrapperManager.Create(context, objResult, null);
                    loggerManager.LogInfo(JsonConvert.SerializeObject(result));
                }
                else
                {
                    result = ResponseWrapperManager.Create(context, null, objResult);
                    loggerManager.LogError(JsonConvert.SerializeObject(result));
                }

                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }
}