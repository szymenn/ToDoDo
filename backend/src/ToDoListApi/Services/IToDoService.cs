using System;
using System.Collections.Generic;
using ToDoListApi.Entities;
using ToDoListApi.Models;

namespace ToDoListApi.Services
{
    public interface IToDoService
    {
        ICollection<ToDoViewModel> AddToDo(ToDoBindingModel toDoModel, Guid userId);
        ICollection<ToDoViewModel> DeleteToDo(Guid toDoId, Guid userId);
        ToDoViewModel UpdateToDo(ToDoBindingModel toDoUpdateModel, Guid toDoId, Guid userId);
        ICollection<ToDoViewModel> GetToDos(Guid userId);
    }
}