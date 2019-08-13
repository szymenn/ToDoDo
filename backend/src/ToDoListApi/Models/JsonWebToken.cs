using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ToDoListApi.Models
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}