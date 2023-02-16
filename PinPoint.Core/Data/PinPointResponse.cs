using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace PinPoint.Core.Data
{
    public class PinPointResponse
    {
        [JsonProperty(PropertyName = "PropertyName")]
        public string? PropertyName { get; set; }



        [JsonProperty(PropertyName = "AttemptedValue")]
        [DataMember(EmitDefaultValue = false)]
        public string? AttemptedValue { get; set; }



        [JsonProperty(PropertyName = "Message")]
        public string? Message { get; set; }



        [JsonProperty(PropertyName = "Code")]
        public string? Code { get; set; }
    }
}
