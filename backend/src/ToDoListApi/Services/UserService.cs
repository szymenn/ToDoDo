using System.Threading.Tasks;
using AutoMapper;
using ToDoListApi.Email;
using ToDoListApi.Models;
using ToDoListApi.Repositories;

namespace ToDoListApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            ITokenService tokenService
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<JsonWebToken> Login(LoginBindingModel userModel)
        {
            return await _userRepository.Login(userModel);
        }

        public async Task<EmailResponse> Register(RegisterBindingModel userModel)
        {
            return await _userRepository.Register(userModel);
        }

        public async Task VerifyEmail(string userId, string emailToken)
        {
            await _userRepository.VerifyEmail(userId, emailToken);
        }

        public UserViewModel GetUser(string userId)
        {
            var userModel = _userRepository.GetUser(userId);
            return _mapper.Map<UserViewModel>(userModel);
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
