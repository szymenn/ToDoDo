namespace ToDoDoApi.Core.Dtos
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}