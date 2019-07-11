using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoListApi.Entities;
using ToDoListApi.Models;

namespace ToDoListApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenManagement _tokenManagement;

        public UserService(
            UserManager<AppUser> userManager,
            IOptions<TokenManagement> tokenManagement
            )
        {
            _userManager = userManager;
            _tokenManagement = tokenManagement.Value;
        }

        public async Task<string> Login(UserBindingModel userModel)
        {
            var user = await GetUser(userModel);
            return GenerateToken(user.UserName);
        }

        public async Task<string> Register(UserBindingModel userModel)
        {
            if (AlreadyExists(userModel.UserName))
            {
                throw new Exception("User with that username already exists");
            }

            var user = new AppUser {UserName = userModel.UserName};
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                return GenerateToken(user.UserName);
            }
            throw new Exception("Registration error");
        }

        private async Task<AppUser> GetUser(UserBindingModel userModel)
        {
            if (!AlreadyExists(userModel.UserName))
            {
                throw new Exception("There's no such user");
            }
            
            var user = _userManager.Users.FirstOrDefault(p => p.UserName == userModel.UserName);
            if (!await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                throw new Exception("Password is incorrect");
            }

            return user;
        }

        private bool AlreadyExists(string userName)
        {
            var user = _userManager.Users.FirstOrDefault(p => p.UserName == userName);
            return user != null;
        }
        
        private string GenerateToken(string userName)
        {
            var claim = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()), 
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials);
            
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

    }
}
