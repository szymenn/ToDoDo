using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Services;

namespace ToDoListApi.Controllers
{
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
    }
}