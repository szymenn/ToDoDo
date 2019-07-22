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
        private readonly IToDoService _toDoService;
        private readonly IMapper _mapper;

        public ToDoListController(IToDoService toDoService, IMapper mapper)
        {
            _toDoService = toDoService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetToDos()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDos = _toDoService.GetToDos(userId);
            var toDosViewModel = _mapper.Map<ICollection<ToDoViewModel>>(toDos);
            return Ok(toDosViewModel);
        }

        [HttpPost]
        public IActionResult AddToDo(ToDoBindingModel toDoModel)
        {
            var toDo = _mapper.Map<ToDo>(toDoModel);
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDos = _toDoService.AddToDo(toDo, userId);
            var toDosViewModel = _mapper.Map<ICollection<ToDoViewModel>>(toDos);
            return Ok(toDosViewModel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDos = _toDoService.DeleteToDo(id, userId);
            var toDosViewModel = _mapper.Map<ICollection<ToDoViewModel>>(toDos);
            return Ok(toDosViewModel);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateToDo([FromBody] ToDoBindingModel toDoModel, [FromRoute] Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDo = _mapper.Map<ToDo>(toDoModel);
            var updatedToDo = _toDoService.UpdateToDo(toDo, id, userId);
            var updatedToDoModel = _mapper.Map<ToDoViewModel>(updatedToDo);
            return Ok(updatedToDoModel);
            
        }
        

    }
}