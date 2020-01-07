using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using ToDoListApi.Data;
using ToDoListApi.Entities;

namespace ToDoListApi.Tests
{
    public class ToDoRepositoryTestBase : IDisposable
    {
        protected ToDoDbContext Context { get; }

        protected ToDoRepositoryTestBase()
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
                    Date = It.IsAny<DateTime>(),
                    UserId = Guid.Parse("12f97655-1c02-4724-ae34-96db34561310"),
                    Id = Guid.Parse("017157a7-747b-4d97-a8eb-15f494553275"),
                    Task = It.IsAny<string>()

                },
                new ToDo
                {
                    Date = It.IsAny<DateTime>(),
                    UserId = Guid.Parse("3872ae50-c148-4ad4-a6c3-8a59e6b4fa33"),
                    Id = Guid.Parse("92566db3-61a7-41fe-8454-e55cff20ea6f"),
                    Task = It.IsAny<string>()

                },
                new ToDo
                {
                    Date = It.IsAny<DateTime>(),
                    UserId = Guid.Parse("b9d82ff5-1bb1-4c55-972a-1e86272438f6"),
                    Id = Guid.Parse("52e8beca-8e79-4485-ac86-31d588b2b7a4"),
                    Task = It.IsAny<string>()

                },
                new ToDo
                {
                    Date = It.IsAny<DateTime>(),
                    UserId = Guid.Parse("86aedc42-a100-4f69-8807-c117e8af039c"),
                    Id = Guid.Parse("b27ab8f1-255a-4379-a1cc-523299f4d133"),
                    Task = It.IsAny<string>()

                }
            };
            
            context.ToDos.AddRange(toDos);
            context.SaveChanges();
        }
    }
}