using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoListApi.Entities;
using ToDoListApi.Models;
using ToDoListApi.Services;

namespace ToDoListApi.Controllers
{
    [Route("todo")]
    [Authorize]
    public class ToDoListController : ControllerBase
    {
        private IToDoService _toDoService;
        private readonly IMapper _mapper;
        private UserManager<AppUser> _userManager;

        public ToDoListController(IToDoService toDoService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _toDoService = toDoService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetToDos()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDos = _toDoService.GetToDos(userId);
            var toDosViewModel = _mapper.Map<ICollection<ToDoDto>>(toDos);
            return Ok(toDosViewModel);
        }

        [HttpPost]
        public IActionResult AddToDo(ToDoDto toDoModel)
        {
            var toDo = _mapper.Map<ToDo>(toDoModel);
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDos = _toDoService.AddToDo(toDo, userId);
            var toDosViewModel = _mapper.Map<ICollection<ToDoDto>>(toDos);
            return Ok(toDosViewModel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDos = _toDoService.DeleteToDo(id, userId);
            var toDosViewModel = _mapper.Map<ICollection<ToDoDto>>(toDos);
            return Ok(toDosViewModel);
        }
        
        [HttpPut]
        public IActionResult UpdateToDo([FromBody] ToDoDto toDoModel)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDo = _mapper.Map<ToDo>(toDoModel);
            var updatedToDo = _toDoService.UpdateToDo(toDo, userId);
            return Ok(updatedToDo);
        }
        

    }
}