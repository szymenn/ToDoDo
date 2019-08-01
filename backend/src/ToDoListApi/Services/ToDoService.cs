using System;
using System.Collections.Generic;
using System.Linq;
using ToDoListApi.Data;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Helpers;
using ToDoListApi.Repositories;

namespace ToDoListApi.Services
{
    public class ToDoService : IToDoService 
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoService(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public ICollection<ToDo> GetToDos(Guid userId)
        {
            return _toDoRepository.GetToDos(userId);
        }

        public ICollection<ToDo> AddToDo(ToDo toDo, Guid userId)
        {
            return _toDoRepository.AddToDo(toDo, userId);
        }

        public ICollection<ToDo> DeleteToDo(Guid toDoId, Guid userId)
        {
            return _toDoRepository.DeleteToDo(toDoId, userId);
        }

        public ToDo UpdateToDo(ToDo toDoUpdate, Guid toDoId, Guid userId)
        {
            return _toDoRepository.UpdateToDo(toDoUpdate, toDoId, userId);
        }
      
    }
}