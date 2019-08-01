using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Helpers;
using ToDoListApi.Models;
using ToDoListApi.Services;

namespace ToDoListApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;

        public UserRepository(
            UserManager<AppUser> userManager,
            ITokenGenerator tokenGenerator,
            IMapper mapper)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }
        
        public async Task<string> Register(RegisterBindingModel userModel)
        {
            if (AlreadyExists(userModel.UserName))
            {
                throw new ResourceAlreadyExistsException(Constants.UserAlreadyExists);
            }

            var user = _mapper.Map<AppUser>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                return _tokenGenerator.GenerateToken(user.UserName, Guid.Parse(user.Id));
            }
            throw new RegistrationException(Constants.RegistrationError);
        }

        public async Task<string> Login(LoginBindingModel userModel)
        {
            var user = await LoginUser(userModel);
            
            return _tokenGenerator.GenerateToken(user.UserName, Guid.Parse(user.Id));
        }

        public AppUser GetUser(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(p => p.Id == userId);
            if (user == null)
            {
                throw new ResourceNotFoundException(Constants.UserNotFound);
            }

            return user;
        }
        
        private async Task<AppUser> LoginUser(LoginBindingModel userModel)
        {
            if (!AlreadyExists(userModel.UserName))
            {
                throw new ResourceNotFoundException(Constants.UserNotFound);
            }
            
            var user = _userManager.Users.FirstOrDefault(p => p.UserName == userModel.UserName);
            if (!await _userManager.CheckPasswordAsync(user, userModel.Password))cd
            {
                throw new PasswordValidationException(Constants.IncorrectPassword);
            }

            return user;
        }
        
        private bool AlreadyExists(string userName)
        {
            var user = _userManager.Users.FirstOrDefault(p => p.UserName == userName);
            return user != null;
        }

    }
}