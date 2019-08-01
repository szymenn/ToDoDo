using System;
using System.Collections.Generic;
using System.Linq;
using ToDoListApi.Data;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Helpers;

namespace ToDoListApi.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _context;

        public ToDoRepository(ToDoDbContext context)
        {
            _context = context;
        }

        public ICollection<ToDo> GetToDos(Guid userId)
        {
            return GetAll(userId);
        }
        
        public ICollection<ToDo> AddToDo(ToDo toDo, Guid userId)
        {
            toDo.UserId = userId;
            _context.ToDos.Add(toDo);
            _context.SaveChanges();

            return GetAll(userId);
        }

        public ICollection<ToDo> DeleteToDo(Guid toDoId, Guid userId)
        {
            var toDo = _context.ToDos.FirstOrDefault(p => p.Id == toDoId && p.UserId == userId);
            if (toDo == null)
            {
                throw new ResourceNotFoundException(Constants.ToDoNotFound);
            }
            _context.ToDos.Remove(toDo);
            _context.SaveChanges();

            return GetAll(userId);
        }

        public ToDo UpdateToDo(ToDo toDoUpdate, Guid toDoId, Guid userId)
        {
            var toDo = _context.ToDos.FirstOrDefault(p => p.Id == toDoId && p.UserId == userId);
            if (toDo == null)
            {
                throw new ResourceNotFoundException(Constants.ToDoNotFound);
            }

            toDo.Task = toDoUpdate.Task;
            toDo.Date = toDoUpdate.Date;
            _context.SaveChanges();
            return toDo;
        }

        private ICollection<ToDo> GetAll(Guid userId)
        {
            return _context.ToDos.Where(p => p.UserId == userId).ToList();
        }
    }
}