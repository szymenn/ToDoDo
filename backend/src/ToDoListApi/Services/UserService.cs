using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Helpers;
using ToDoListApi.Models;
using ToDoListApi.Repositories;

namespace ToDoListApi.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;

        public UserService(
            ITokenGenerator tokenGenerator,
            IUserRepository userRepository
        )
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<string> Login(LoginBindingModel userModel)
        {
            return await _userRepository.Login(userModel);
        }

        public async Task<string> Register(RegisterBindingModel userModel)
        {
            return await _userRepository.Register(userModel);
        }

        public AppUser GetUser(string userId)
        {
            return _userRepository.GetUser(userId);
        }
    }
}
