using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ToDoListApi.Models
{
    public class JsonWebToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("expires")]
        public int Expires { get; set; }
}
}