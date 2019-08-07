using System.Threading.Tasks;
using ToDoListApi.Entities;
using ToDoListApi.Models;

namespace ToDoListApi.Repositories
{
    public interface IUserRepository
    {
        Task<JsonWebToken> Register(RegisterBindingModel userModel);
        Task<JsonWebToken> Login(LoginBindingModel userModel);
        AppUser GetUser(string userId);
        
    }
}