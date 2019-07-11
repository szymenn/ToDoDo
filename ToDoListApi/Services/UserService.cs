using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ToDoListApi.Entities;

namespace ToDoListApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public UserService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> Login(string userName, string password)
        {
            var user = _userManager.Users.FirstOrDefault(p => p.UserName == userName);
            if (user == null)
            {
                throw new NotImplementedException();
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                throw new NotImplementedException();   
            }
            
            throw new NotImplementedException();
        }
        
    }
}
