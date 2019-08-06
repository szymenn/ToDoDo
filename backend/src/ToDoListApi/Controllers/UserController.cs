using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Helpers;
using ToDoListApi.Models;
using ToDoListApi.Services;

namespace ToDoListApi.Controllers
{
    [Route(ApiRoutes.User)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(ApiRoutes.Login)]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel userModel)
        {
            var token =  await _userService.Login(userModel);

            return Ok(new
            {
                Token = token
            });
        }
        
        [HttpPost(ApiRoutes.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterBindingModel userModel)
        {
            var token =  await _userService.Register(userModel);

            return Ok(new
            {
                Token = token
            });
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userModel = _userService.GetUser(userId);
            return Ok(userModel);
        }
        
            
    }
}