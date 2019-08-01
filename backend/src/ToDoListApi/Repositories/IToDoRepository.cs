using System;
using System.Collections.Generic;
using ToDoListApi.Entities;

namespace ToDoListApi.Repositories
{
    public interface IToDoRepository
    {
        ICollection<ToDo> GetToDos(Guid userId);
        ICollection<ToDo> AddToDo(ToDo toDo, Guid userId);
        ICollection<ToDo> DeleteToDo(Guid toDoId, Guid userId);
        ToDo UpdateToDo(ToDo toDoUpdate, Guid toDoId, Guid userId);
    }
}