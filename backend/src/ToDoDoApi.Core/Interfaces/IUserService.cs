using System.Threading.Tasks;
using ToDoDoApi.Core.Dtos;
using ToDoDoApi.Core.Entities;

namespace ToDoDoApi.Core.Interfaces
{
    public interface IUserService
    {
        Task<JsonWebToken> Login(AppUser user, string password);
        Task<EmailResponse> Register(AppUser user, string password);
        Task VerifyEmail(string userId, string emailToken);
        
        AppUser GetUser(string userId);
        JsonWebToken RefreshAccessToken(string token);
        void RevokeRefreshToken(string token);
        
    }
}