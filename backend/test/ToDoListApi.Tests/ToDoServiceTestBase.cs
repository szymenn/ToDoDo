using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Entities;

namespace ToDoListApi.Tests
{
    public class ToDoServiceTestBase : IDisposable
    {
        protected ToDoDbContext Context { get; }

        protected ToDoServiceTestBase()
        {
            var options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            Context = new ToDoDbContext(options);

            Context.Database.EnsureCreated();
            
            Initialize(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

        private void Initialize(ToDoDbContext context)
        {
            var toDos = new[]
            {
                new ToDo
                {
                    Task = "Task 1",
                    Date = new DateTime(2013, 3, 5),
                    UserId = Guid.Parse("69367316-4142-484f-ac9e-3f721181a8b5")
                },
                new ToDo
                {
                    Task = "Task 2",
                    Date = new DateTime(2012, 7, 5),
                    UserId = Guid.Parse("16deab94-2d83-4875-a28e-965a67a89d3e")
                },
                new ToDo
                {
                    Task = "Task 3",
                    Date = new DateTime(2013, 5, 25),
                    UserId = Guid.Parse("79c98035-bbe2-4cee-8b9b-f1a52210fd8e")
                },
            };
            
            context.ToDos.AddRange(toDos);
            context.SaveChanges();
        }
        
        
    }
}