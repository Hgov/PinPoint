using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PinPoint.Infrastructure.LoggerService;

namespace PinPoint.Infrastructure.Response
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;
        private readonly LoggerManager loggerManager;
        public ResponseWrapper(RequestDelegate next)
        {
            loggerManager = new LoggerManager();
            _next = next;
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
                int? statusCode = (int?)readToEndSingle["statusCode"];
                //readToEndSingle.Property("formatters").Remove();
                //readToEndSingle.Property("contentTypes").Remove();
                //readToEndSingle.Property("declaredType").Remove();
                //readToEndSingle.Property("statusCode").Remove();

                object objResult = JsonConvert.DeserializeObject<ResponseWrapperData>(readToEndSingle.ToString());
                CommonApiResponse? result = null;
                if (statusCode >= 100 && statusCode < 200)
                {
                    result = CommonApiResponse.Create((HttpStatusCode)statusCode, objResult, null, null, null, null);
                    loggerManager.LogInfo(JsonConvert.SerializeObject(result));
                }
                else if (statusCode >= 200 && statusCode < 300)
                {
                    result = CommonApiResponse.Create((HttpStatusCode)statusCode, null, objResult, null, null, null);
                    loggerManager.LogInfo(JsonConvert.SerializeObject(result));
                }
                else if (statusCode >= 300 && statusCode < 400)
                {
                    result = CommonApiResponse.Create((HttpStatusCode)statusCode, null, null, objResult, null, null);
                    loggerManager.LogInfo(JsonConvert.SerializeObject(result));
                }
                else if (statusCode >= 400 && statusCode < 500)
                {
                    result = CommonApiResponse.Create((HttpStatusCode)statusCode, null, null, null, objResult, null);
                    loggerManager.LogError(JsonConvert.SerializeObject(result));
                }
                else if (statusCode >= 500 && statusCode < 600)
                {
                    result = CommonApiResponse.Create((HttpStatusCode)statusCode, null, null, null, null, objResult);
                    loggerManager.LogError(JsonConvert.SerializeObject(result));
                }


                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }

    }
    //Informational responses(100 – 199)
    //Successful responses(200 – 299)
    //Redirection messages(300 – 399)
    //Client error responses(400 – 499)
    //Server error responses(500 – 599)

    [DataContract]
    public class CommonApiResponse
    {
        public static CommonApiResponse Create(HttpStatusCode statusCode, object? InformationalMessage = null, object? SuccessfulMessage = null, object? RedirectionMessage = null, object? ClientErrorMessage = null, object? ServerErrorMessage = null)
        {
            return new CommonApiResponse(statusCode, InformationalMessage, SuccessfulMessage, RedirectionMessage, ClientErrorMessage, ServerErrorMessage);
        }
        [DataMember]
        public string Version => "V1.0";
        [DataMember(EmitDefaultValue = false)]
        public int StatusCode { get; set; }

        [DataMember]
        public string RequestId { get; }


        [DataMember(EmitDefaultValue = false)]
        public object InformationalMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object SuccessfulMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object RedirectionMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object ClientErrorMessage { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object ServerErrorMessage { get; set; }



        protected CommonApiResponse(HttpStatusCode statusCode, object? InformationalMessage = null, object? SuccessfulMessage = null, object? RedirectionMessage = null, object? ClientErrorMessage = null, object? ServerErrorMessage = null)
        {
            this.RequestId = Guid.NewGuid().ToString();
            this.StatusCode = (int)statusCode;
            this.SuccessfulMessage = SuccessfulMessage;
            this.ClientErrorMessage = ClientErrorMessage;
            this.InformationalMessage = InformationalMessage;
            this.RedirectionMessage = RedirectionMessage;
            this.ServerErrorMessage = ServerErrorMessage;
        }
    }

    public class ResponseWrapperData
    {
        public string value
        {
            get;
            set;
        }
    }

}
