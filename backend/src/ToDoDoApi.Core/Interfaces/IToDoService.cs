using System;
using System.Collections.Generic;
using ToDoDoApi.Core.Entities;

namespace ToDoDoApi.Core.Interfaces
{
    public interface IToDoService
    {
        ICollection<ToDo> AddToDo(ToDo toDo, Guid userId);
        ICollection<ToDo> DeleteToDo(Guid toDoId, Guid userId);
        ToDo UpdateToDo(ToDo toDoUpdate, Guid toDoId, Guid userId);
        ICollection<ToDo> GetToDos(Guid userId);
    }
}