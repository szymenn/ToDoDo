using System.Threading.Tasks;
using ToDoDoApi.Core.Dtos;
using ToDoDoApi.Core.Entities;
using ToDoDoApi.Core.Interfaces;

namespace ToDoDoApi.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(
            IUserRepository userRepository,
            ITokenService tokenService
        )
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<JsonWebToken> Login(AppUser user, string password)
        {
            return await _userRepository.Login(user, password);
        }

        public async Task<EmailResponse> Register(AppUser user, string password)
        {
            return await _userRepository.Register(user, password);
        }

        public async Task VerifyEmail(string userId, string emailToken)
        {
            await _userRepository.VerifyEmail(userId, emailToken);
        }

        AppUser IUserService.GetUser(string userId)
        {
            return _userRepository.GetUser(userId);
        }

        public JsonWebToken RefreshAccessToken(string token)
        {
            return _tokenService.RefreshAccessToken(token);
        }

        public void RevokeRefreshToken(string token)
        {
            _tokenService.RevokeRefreshToken(token);
        }
    }
}