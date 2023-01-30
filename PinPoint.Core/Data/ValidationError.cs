using Newtonsoft.Json;

namespace PinPoint.Core.Data
{
    public class ValidationError
    {
        [JsonProperty(PropertyName = "PropertyName")]
        public string? propertyName { get; set; }
        [JsonProperty(PropertyName = "ErrorMessage")]
        public string? errorMessage { get; set; }
        [JsonProperty(PropertyName = "ErrorCode")]
        public string? errorCode { get; set; }
    }
}
