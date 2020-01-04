using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoDoApi.Core.Entities;
using ToDoDoApi.Core.Interfaces;
using ToDoDoApi.Web.Models;

namespace ToDoDoApi.Web.Controllers
{
    [Route("todos")]
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
            var toDosModel = _mapper.Map<ToDoApiModel>(toDos);
            return Ok(toDosModel);
        }

        [HttpPost]
        public IActionResult AddToDo(ToDoBindingModel toDoModel)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDo = _mapper.Map<ToDo>(toDoModel);
            var toDos = _toDoService.AddToDo(toDo, userId);
            var toDosModel = _mapper.Map<ICollection<ToDoApiModel>>(toDos);
            return Ok(toDosModel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDos = _toDoService.DeleteToDo(id, userId);
            var toDosModel = _mapper.Map<ICollection<ToDoApiModel>>(toDos);
            return Ok(toDosModel);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateToDo([FromBody] ToDoBindingModel toDoModel, [FromRoute] Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var toDo = _mapper.Map<ToDo>(toDoModel);
            var updatedToDo = _toDoService.UpdateToDo(toDo, id, userId);
            var updatedToDoModel = _mapper.Map<ToDoApiModel>(updatedToDo);
            return Ok(updatedToDoModel);
        }
        

    }
}