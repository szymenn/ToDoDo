using System.Threading.Tasks;
using ToDoListApi.Email;
using ToDoListApi.Models;

namespace ToDoListApi.Services
{
    public interface IUserService
    {
        Task<JsonWebToken> Login(LoginBindingModel userModel);
        Task<EmailResponse> Register(RegisterBindingModel userModel);
        Task VerifyEmail(string userId, string emailToken);
        
        UserViewModel GetUser(string userId);
        JsonWebToken RefreshAccessToken(string token);
        void RevokeRefreshToken(string token);
        
    }
}