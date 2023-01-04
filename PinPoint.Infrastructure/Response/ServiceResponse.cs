using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPoint.Infrastructure.Response
{
    public class ServiceResponse<TEntity> where TEntity : class
    {
        [JsonProperty(PropertyName = "StatusCode")]
        public int? statusCode { get; set; }
        [JsonProperty(PropertyName = "Error")]
        public IEnumerable<Error>? error { get; set; }
        [JsonProperty(PropertyName = "Entity")]
        public List<TEntity> Entity { get; set; }
    }
    public class Error
    {
        [JsonProperty(PropertyName = "PropertyName")]
        public string? propertyName { get; set; }
        [JsonProperty(PropertyName = "ErrorMessage")]
        public string? errorMessage { get; set; }
        [JsonProperty(PropertyName = "ErrorCode")]
        public string? errorCode { get; set; }
    }
}
