namespace ToDoListApi.Helpers
{
    public static class Constants
    {
        public const string ToDoDbConnectionString = "ToDoDbConnectionString";
        public const string JwtSettings = "JwtSettings";
        public const string EmailVerificationSettings = "EmailVerificationSettings";
        public const string UserStoreConnectionString = "UserStoreDb";
        public const string NotFound = "Not Found";
        public const string Conflict = "Conflict";
        public const string BadRequest = "Bad Request";
        public const string ToDoNotFound = "Todo with specified id does not exist";
        public const string UserNotFound = "User with specified username does not exist";
        public const string UserAlreadyExists = "User with specified username already exists";
        public const string IncorrectPassword = "Password is incorrect";
        public const string RegistrationError =
            "An error occurred during registration, please make sure an email address is not already signed up";
        public const string InternalServerError = "Internal Server error";
        public const string InternalServerErrorDetail = "An unexpected error occurred";
        public const string AllowSpecificOrigins = "_allowSpecificOrigins";
        public const string TokenStoreDb = "TokenStoreDb";
        public const string RedirectSuccess = "https://szymenn.github.io/ToDoDo-frontend";
        public const string EmailVerificationException = "Unable to verify email";
        public const string LoginFailed =
            "Login Failed, please make sure your email is verified and provided password is correct";

        public const string ApiUrl = "https://to-do-do.herokuapp.com";
        public const string EmailSenderException =
            "An error occured while sending confirmation email, please try again";
    }
}