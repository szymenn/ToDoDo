using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Helpers;
using ToDoListApi.Models;
using ToDoListApi.Services;

namespace ToDoListApi.Controllers
{
    [Route(ApiRoutes.User)]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(ApiRoutes.Login)]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel userModel)
        {
            var tokenResponse =  await _userService.Login(userModel);

            return Ok(new
            {
                Response = tokenResponse
            });
        }
        
        [HttpPost(ApiRoutes.Register)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterBindingModel userModel)
        {
            var tokenResponse =  await _userService.Register(userModel);

            return Ok(new
            {
                Response = tokenResponse
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
        public IActionResult RefreshAccessToken([FromBody] string  token)
        {
            var tokenResponse = _userService.RefreshAccessToken(token);
            return Ok(new
            {
                Response = tokenResponse
            });
        }

        [HttpPost("tokens/{token}/revoke")]
        public IActionResult RevokeRefreshToken([FromRoute] string token)
        {
            _userService.RevokeRefreshToken(token);
            return NoContent();
        }
            
    }
}