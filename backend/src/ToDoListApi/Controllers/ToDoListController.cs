using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Helpers;
using ToDoListApi.Models;
using ToDoListApi.Services;

namespace ToDoListApi.Controllers
{
    [Route("todos")]
    [Authorize]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoListController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet]
        public IActionResult GetToDos()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDosModel = _toDoService.GetToDos(userId);
            return Ok(toDosModel);
        }

        [HttpPost]
        public IActionResult AddToDo(ToDoBindingModel toDoModel)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDosModel = _toDoService.AddToDo(toDoModel, userId);
            return Ok(toDosModel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDosModel = _toDoService.DeleteToDo(id, userId);
            return Ok(toDosModel);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateToDo([FromBody] ToDoBindingModel toDoModel, [FromRoute] Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var updatedToDoModel = _toDoService.UpdateToDo(toDoModel, id, userId);
            return Ok(updatedToDoModel);
        }
        

    }
}