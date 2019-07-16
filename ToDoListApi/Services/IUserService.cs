using System.Threading.Tasks;
using ToDoListApi.Entities;
using ToDoListApi.Models;

namespace ToDoListApi.Services
{
    public interface IUserService
    {
        Task<string> Login(UserBindingModel userModel);
        Task<string> Register(UserBindingModel userModel);
        AppUser GetUser(string userId);
    }
}