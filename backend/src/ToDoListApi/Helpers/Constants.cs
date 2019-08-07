namespace ToDoListApi.Helpers
{
    public static class Constants
    {
        public const string ToDoDbConnectionString = "ToDoDbConnectionString";
        public const string JwtSettings = "JwtSettings";
        public const string UserStoreConnectionString = "UserStoreDb";
        public const string NotFound = "Not Found";
        public const string Conflict = "Conflict";
        public const string BadRequest = "Bad Request";
        public const string ToDoNotFound = "Todo with specified id does not exist";
        public const string UserNotFound = "User with specified username does not exist";
        public const string UserAlreadyExists = "User with specified username already exists";
        public const string IncorrectPassword = "Password is incorrect";
        public const string RegistrationError = "An error occurred during registration";
        public const string InternalServerError = "Internal Server error";
        public const string InternalServerErrorDetail = "An unexpected error occurred";
        public const string AllowSpecificOrigins = "_allowSpecificOrigins";
        public const string TokenStoreDb = "TokenStoreDb";
    }
}