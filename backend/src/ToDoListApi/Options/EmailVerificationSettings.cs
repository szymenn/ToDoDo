using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ToDoListApi.Options
{
    [JsonObject("EmailVerificationSettings")]
    public class EmailVerificationSettings
    {
        [JsonProperty("ApiKey")]
        public string ApiKey { get; set; }
        [JsonProperty("FromEmail")]
        public string FromEmail { get; set; }
        [JsonProperty("FromName")]
        public string FromName { get; set; }
        [JsonProperty("Subject")]
        public string Subject { get; set; }
    }
}