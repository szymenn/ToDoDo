using System.Threading.Tasks;
using ToDoListApi.Models;

namespace ToDoListApi.Services
{
    public interface IUserService
    {
        Task<string> Login(LoginBindingModel userModel);
        Task<string> Register(RegisterBindingModel userModel);
        UserViewModel GetUser(string userId);
    }
}