using System.Threading.Tasks;
using ToDoListApi.Entities;
using ToDoListApi.Models;

namespace ToDoListApi.Repositories
{
    public interface IUserRepository
    {
        Task<string> Register(RegisterBindingModel userModel);
        Task<string> Login(LoginBindingModel userModel);
        AppUser GetUser(string userId);
        
    }
}