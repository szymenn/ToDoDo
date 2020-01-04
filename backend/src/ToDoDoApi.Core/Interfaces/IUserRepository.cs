using System.Threading.Tasks;
using ToDoDoApi.Core.Dtos;
using ToDoDoApi.Core.Entities;

namespace ToDoDoApi.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<EmailResponse> Register(AppUser user, string password);
        Task<JsonWebToken> Login(AppUser user, string password);
        Task VerifyEmail(string userId, string emailToken);
        AppUser GetUser(string userId);
        
    }
}