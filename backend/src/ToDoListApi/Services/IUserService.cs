using System.Threading.Tasks;
using ToDoListApi.Models;

namespace ToDoListApi.Services
{
    public interface IUserService
    {
        Task<JsonWebToken> Login(LoginBindingModel userModel);
        Task<JsonWebToken> Register(RegisterBindingModel userModel);
        UserViewModel GetUser(string userId);
        JsonWebToken RefreshAccessToken(string token);
        void RevokeRefreshToken(string token);
    }
}