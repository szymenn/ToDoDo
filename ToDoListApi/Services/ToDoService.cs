using System;
using System.Collections.Generic;
using System.Linq;
using ToDoListApi.Data;
using ToDoListApi.Entities;

namespace ToDoListApi.Services
{
    public class ToDoService : IToDoService 
    {
        private readonly ToDoDbContext _context;

        public ToDoService(ToDoDbContext context)
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
                throw new Exception("ToDo with that id does not exist");
            }
            _context.ToDos.Remove(toDo);
            _context.SaveChanges();

            return GetAll(userId);
        }

        public ToDo UpdateToDo(ToDo toDoUpdate, Guid userId)
        {
            var toDo = _context.ToDos.FirstOrDefault(p => p.Id == toDoUpdate.Id && p.UserId == userId);
            if (toDo == null)
            {
                throw new Exception("Todo with that id does not exist");
            }

            toDo.Task = toDoUpdate.Task;
            _context.SaveChanges();
            return toDo;
        }
        
        
        private ICollection<ToDo> GetAll(Guid userId)
        {
            return _context.ToDos.Where(p => p.UserId == userId).ToList();
        }
    }
}