using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoDoApi.Core.Entities;
using ToDoDoApi.Core.Helpers;
using ToDoDoApi.Core.Interfaces;
using ToDoDoApi.Web.Models;
using ToDoListApi.Models;

namespace ToDoDoApi.Web.Controllers
{
    [Route("user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel userModel)
        {
            var user = _mapper.Map<AppUser>(userModel);
            var authResponse =  await _userService.Login(user, userModel.Password);

            return Ok(new
            {
                Tokens = authResponse
            });
        }
        
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterBindingModel userModel)
        {
            var user = _mapper.Map<AppUser>(userModel);
            var response = await _userService.Register(user, userModel.Password);
            return Ok(new
            {
                Response = response
            });
        }
        
        [HttpGet]
        public IActionResult GetUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userService.GetUser(userId);
            var userModel = _mapper.Map<UserApiModel>(user);
            return Ok(userModel);
        }

        [HttpPost("tokens/refresh")]
        [AllowAnonymous]
        public IActionResult RefreshAccessToken([FromBody] string token)
        {
            var authResponse = _userService.RefreshAccessToken(token);
            return Ok(new
            {
                Tokens = authResponse
            });
        }

        [HttpPost("tokens/revoke")]
        public IActionResult RevokeRefreshToken([FromBody] string token)
        {
            _userService.RevokeRefreshToken(token);
            return NoContent();
        }

        [HttpGet("email/verify")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string userId, string confirmationToken)
        {
            await _userService.VerifyEmail(userId, confirmationToken);
            return Redirect(Constants.RedirectSuccess);
        }
        
    }
}