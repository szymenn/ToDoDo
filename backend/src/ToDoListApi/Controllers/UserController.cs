using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Helpers;
using ToDoListApi.Models;
using ToDoListApi.Services;

namespace ToDoListApi.Controllers
{
    [Route("user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel userModel)
        {
            var authResponse =  await _userService.Login(userModel);

            return Ok(new
            {
                Tokens = authResponse
            });
        }
        
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterBindingModel userModel)
        {
            var authResponse =  await _userService.Register(userModel);

            return Ok(new
            {
                Tokens = authResponse
            });
        }
        
        [HttpGet]
        public IActionResult GetUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userModel = _userService.GetUser(userId);
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
            
    }
}