using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net;
using System.Runtime.Serialization;

namespace PinPoint.Infrastructure.ResponseWrapper
{

    //Informational responses(100 – 199)
    //Successful responses(200 – 299)
    //Redirection messages(300 – 399)
    //Client error responses(400 – 499)
    //Server error responses(500 – 599)

    [DataContract]
    public class ResponseWrapperManager
    {

        [DataMember]
        public string Version => "V1.0.0";

        [DataMember]
        public string requestUrl { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public HttpStatusCode StatusCode { get; set; }

        [DataMember]
        public string RequestId { get; }

        [DataMember(EmitDefaultValue = false)]
        public object Result { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public object Error { get; set; }
        protected ResponseWrapperManager(HttpContext context, object? Result = null, object? Error = null)
        {
            this.requestUrl = context.Request.GetDisplayUrl();
            this.RequestId = Guid.NewGuid().ToString();
            this.StatusCode = (HttpStatusCode)context.Response.StatusCode;
            this.Result = Result;
            this.Error = Error;
        }

        public static ResponseWrapperManager Create(HttpContext context, object? Result = null, object? Error = null)
        {
            return new ResponseWrapperManager(context, Result, Error);
        }
    }

}
