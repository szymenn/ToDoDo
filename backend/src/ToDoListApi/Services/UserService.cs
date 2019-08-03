using System.Threading.Tasks;
using AutoMapper;
using ToDoListApi.Entities;
using ToDoListApi.Models;
using ToDoListApi.Repositories;

namespace ToDoListApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper            
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<string> Login(LoginBindingModel userModel)
        {
            return await _userRepository.Login(userModel);
        }

        public async Task<string> Register(RegisterBindingModel userModel)
        {
            return await _userRepository.Register(userModel);
        }

        public UserViewModel GetUser(string userId)
        {
            var userModel = _userRepository.GetUser(userId);
            return _mapper.Map<UserViewModel>(userModel);
        }
    }
}
