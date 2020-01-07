using System;
using System.Collections.Generic;
using Moq;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Repositories;
using Xunit;

namespace ToDoListApi.Tests
{
    public class ToDoRepositoryTests : ToDoRepositoryTestBase
    {
        private readonly ToDoRepository _toDoRepository;

        public ToDoRepositoryTests()
        {
            _toDoRepository = new ToDoRepository(Context);
        }
        
        [Fact]
        public void GetToDos_ByDefault_ReturnsToDosCollection()
        {
            var result = _toDoRepository.GetToDos(Guid.Parse("12f97655-1c02-4724-ae34-96db34561310"));

            Assert.IsAssignableFrom<ICollection<ToDo>>(result);
        }

        [Fact]
        public void AddTodo_ByDefault_ReturnsToDosCollection()
        {
            var toDo = new ToDo
            {
                Date = It.IsAny<DateTime>(),
                Task = It.IsAny<string>()
            };

            var result = _toDoRepository.AddToDo
                (toDo, Guid.Parse("12f97655-1c02-4724-ae34-96db34561310"));

            Assert.IsAssignableFrom<ICollection<ToDo>>(result);
        }

        [Fact]
        public void DeleteToDo_ByDefault_ReturnsToDosCollection()
        {
            var result = _toDoRepository.DeleteToDo
            (Guid.Parse("b27ab8f1-255a-4379-a1cc-523299f4d133"),
                Guid.Parse("86aedc42-a100-4f69-8807-c117e8af039c"));

            Assert.IsAssignableFrom<ICollection<ToDo>>(result);
        }

        [Fact]
        public void UpdateToDo_ByDefault_ReturnsUpdatedToDo()
        {
            var toDo = new ToDo
            {
                Task = It.IsAny<string>(),
                Date = It.IsAny<DateTime>()
            };

            var result = _toDoRepository.UpdateToDo
            (toDo, Guid.Parse("b27ab8f1-255a-4379-a1cc-523299f4d133"),
                Guid.Parse("86aedc42-a100-4f69-8807-c117e8af039c"));

            Assert.IsType<ToDo>(result);
        }

        [Fact]
        public void DeleteToDo_WhenToDoNotFound_ThrowsResourceNotFoundException()
        {
            Assert.Throws<ResourceNotFoundException>(
                () => _toDoRepository.DeleteToDo
                (It.Is<Guid>(m => m != Guid.Parse("b27ab8f1-255a-4379-a1cc-523299f4d133")),
                    It.Is<Guid>(m => m != Guid.Parse("86aedc42-a100-4f69-8807-c117e8af039c"))));
        }

        [Fact]
        public void UpdateToDo_WhenToDoNotFound_ThrowsResourceNotFoundException()
        {
            Assert.Throws<ResourceNotFoundException>(
                () => _toDoRepository.UpdateToDo(It.IsAny<ToDo>(),
                    It.Is<Guid>(m => m != Guid.Parse("b27ab8f1-255a-4379-a1cc-523299f4d133")),
                    It.Is<Guid>(m => m != Guid.Parse("86aedc42-a100-4f69-8807-c117e8af039c"))));
        }
    }
}