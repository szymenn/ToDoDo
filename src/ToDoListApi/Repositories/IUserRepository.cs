using System.Threading.Tasks;
using ToDoListApi.Email;
using ToDoListApi.Entities;
using ToDoListApi.Models;

namespace ToDoListApi.Repositories
{
    public interface IUserRepository
    {
        Task<EmailResponse> Register(RegisterBindingModel userModel);
        Task<JsonWebToken> Login(LoginBindingModel userModel);
        Task VerifyEmail(string userId, string emailToken);
        AppUser GetUser(string userId);
        
    }
}