using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Helpers;
using ToDoListApi.Models;

namespace ToDoListApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;

        public UserService(
            UserManager<AppUser> userManager,
            ITokenGenerator tokenGenerator
        )
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> Login(LoginBindingModel userModel)
        {
            var user = await LoginUser(userModel);

            return _tokenGenerator.GenerateToken(user.UserName, Guid.Parse(user.Id));
        }

        public async Task<string> Register(RegisterBindingModel userModel)
        {
            if (AlreadyExists(userModel.UserName))
            {
                throw new ResourceAlreadyExistsException(Constants.UserAlreadyExists);
            }

            var user = new AppUser {UserName = userModel.UserName};
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                return _tokenGenerator.GenerateToken(user.UserName, Guid.Parse(user.Id));
            }
            throw new RegistrationException(Constants.RegistrationError);
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
            if (!await _userManager.CheckPasswordAsync(user, userModel.Password))
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
