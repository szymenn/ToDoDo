using System;
using System.Collections.Generic;
using AutoMapper;
using ToDoListApi.Entities;
using ToDoListApi.Models;
using ToDoListApi.Repositories;

namespace ToDoListApi.Services
{
    public class ToDoService : IToDoService 
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IMapper _mapper;

        public ToDoService(IToDoRepository toDoRepository, IMapper mapper)
        {
            _toDoRepository = toDoRepository;
            _mapper = mapper;
        }

        public ICollection<ToDoViewModel> GetToDos(Guid userId)
        {
            var toDos = _toDoRepository.GetToDos(userId);
            return _mapper.Map<ICollection<ToDoViewModel>>(toDos);
        }

        public ICollection<ToDoViewModel> AddToDo(ToDoBindingModel toDoModel, Guid userId)
        {
            var toDo = _mapper.Map<ToDo>(toDoModel);
            var toDos = _toDoRepository.AddToDo(toDo, userId);
            return _mapper.Map<ICollection<ToDoViewModel>>(toDos);
        }

        public ICollection<ToDoViewModel> DeleteToDo(Guid toDoId, Guid userId)
        {
            var toDos = _toDoRepository.DeleteToDo(toDoId, userId);
            return _mapper.Map<ICollection<ToDoViewModel>>(toDos);
        }

        public ToDoViewModel UpdateToDo(ToDoBindingModel toDoUpdateModel, Guid toDoId, Guid userId)
        {
            var toDoUpdate = _mapper.Map<ToDo>(toDoUpdateModel);
            var toDo = _toDoRepository.UpdateToDo(toDoUpdate, toDoId, userId);
            return _mapper.Map<ToDoViewModel>(toDo);
        }
      
    }
}