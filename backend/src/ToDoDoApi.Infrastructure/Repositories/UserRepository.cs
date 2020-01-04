using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ToDoDoApi.Core.Dtos;
using ToDoDoApi.Core.Entities;
using ToDoDoApi.Core.Exceptions;
using ToDoDoApi.Core.Helpers;
using ToDoDoApi.Core.Interfaces;

namespace ToDoDoApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<AppUser> _signInManager;

        public UserRepository(
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            IEmailSender emailSender, 
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }
        
        public async Task<EmailResponse> Register(AppUser user, string password)
        {
            if (AlreadyExists(user.UserName))
            {
                throw new ResourceAlreadyExistsException(Constants.UserAlreadyExists);
            }

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var appUser = _userManager.Users.FirstOrDefault(p => p.UserName == user.UserName);
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                return await _emailSender.SendEmailAsync(appUser, confirmationToken);
            }
            throw new RegistrationException(Constants.RegistrationError);
        }

        public async Task<JsonWebToken> Login(AppUser user, string password)
        {
            var loggedUser = await LoginUser(user.UserName, password);
            
            return _tokenService.CreateAccessToken(loggedUser.UserName, Guid.Parse(user.Id));
        }

        public async Task VerifyEmail(string userId, string emailToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ResourceNotFoundException(Constants.UserNotFound);
            }

            var result = await _userManager.ConfirmEmailAsync(user, emailToken);
            if (!result.Succeeded)
            {
                throw new EmailVerificationException(Constants.EmailVerificationException);
            }
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
        
        private async Task<AppUser> LoginUser(string userName, string password)
        {
            if (!AlreadyExists(userName))
            {
                throw new ResourceNotFoundException(Constants.UserNotFound);
            }
            
            var loggedUser = _userManager.Users.FirstOrDefault(p => p.UserName == userName);
            var result = await _signInManager.PasswordSignInAsync(loggedUser, password, false, false);
            if (!result.Succeeded)
            {
                throw new LoginException(Constants.LoginFailed);
            }

            return loggedUser;
        }
        
        private bool AlreadyExists(string userName)
        {
            var user = _userManager.Users.FirstOrDefault(p => p.UserName == userName);
            return user != null;
        }

    }
}