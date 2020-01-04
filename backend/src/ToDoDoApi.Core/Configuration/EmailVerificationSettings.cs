using Newtonsoft.Json;

namespace ToDoDoApi.Core.Configuration
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