using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToDoListApi.Email;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Helpers;
using ToDoListApi.Models;
using ToDoListApi.Services;
using System.Web;

namespace ToDoListApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<AppUser> _signInManager;

        public UserRepository(
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            IMapper mapper,
            IEmailSender emailSender, 
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }
        
        public async Task<EmailResponse> Register(RegisterBindingModel userModel)
        {
            if (AlreadyExists(userModel.UserName))
            {
                throw new ResourceAlreadyExistsException(Constants.UserAlreadyExists);
            }

            var user = _mapper.Map<AppUser>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                var appUser = _userManager.Users.FirstOrDefault(p => p.UserName == userModel.UserName);
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                return await _emailSender.SendEmailAsync(appUser, confirmationToken);
            }
            throw new RegistrationException(Constants.RegistrationError);
        }

        public async Task<JsonWebToken> Login(LoginBindingModel userModel)
        {
            var user = await LoginUser(userModel);
            
            return _tokenService.CreateAccessToken(user.UserName, Guid.Parse(user.Id));
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
        
        private async Task<AppUser> LoginUser(LoginBindingModel userModel)
        {
            if (!AlreadyExists(userModel.UserName))
            {
                throw new ResourceNotFoundException(Constants.UserNotFound);
            }
            
            var user = _userManager.Users.FirstOrDefault(p => p.UserName == userModel.UserName);
            var result = await _signInManager.PasswordSignInAsync(user, userModel.Password, false, false);
            if (!result.Succeeded)
            {
                throw new LoginException(Constants.LoginFailed);
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