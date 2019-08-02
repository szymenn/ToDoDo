using System;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Models;
using ToDoListApi.Repositories;
using ToDoListApi.Services;
using Xunit;

namespace ToDoListApi.Tests
{
    public class ToDoServiceTests 
    {
        [Fact]
        public void GetToDos_ByDefault_ReturnsToDoViewModelList()
        {
            var toDoRepositoryStub = new Mock<IToDoRepository>();
            var mapperStub = new Mock<IMapper>();
            toDoRepositoryStub.Setup(e => e.GetToDos(It.IsAny<Guid>()))
                .Returns(new List<ToDo>());
            mapperStub.Setup(e => e.Map<ICollection<ToDoViewModel>>(It.IsAny<ICollection<ToDo>>()))
                .Returns(new List<ToDoViewModel>());

            var service = new ToDoService
                (toDoRepositoryStub.Object, mapperStub.Object);

            var result = service.GetToDos(It.IsAny<Guid>());
            Assert.IsType<List<ToDoViewModel>>(result);
        }

        [Fact]
        public void AddToDo_ByDefault_ReturnsToDoViewModelList()
        {
            var toDoRepositoryStub = new Mock<IToDoRepository>();
            var mapperStub = new Mock<IMapper>();
            toDoRepositoryStub.Setup(e => e.AddToDo(It.IsAny<ToDo>(), It.IsAny<Guid>()))
                .Returns(new List<ToDo>());
            mapperStub.Setup(e => e.Map<ICollection<ToDoViewModel>>(It.IsAny<ICollection<ToDo>>()))
                .Returns(new List<ToDoViewModel>());
            
            var service = new ToDoService
                (toDoRepositoryStub.Object, mapperStub.Object);

            var result = service.AddToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>());
            Assert.IsType<List<ToDoViewModel>>(result);
        }

        [Fact]
        public void DeleteToDo_ByDefault_ReturnsToDoViewModelList()
        {
            var toDoRepositoryStub = new Mock<IToDoRepository>();
            var mapperStub = new Mock<IMapper>();
            toDoRepositoryStub.Setup(e => e.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(new List<ToDo>());
            mapperStub.Setup(e => e.Map<ICollection<ToDoViewModel>>(It.IsAny<ICollection<ToDo>>()))
                .Returns(new List<ToDoViewModel>());
            
            var service = new ToDoService
                (toDoRepositoryStub.Object, mapperStub.Object);

            var result = service.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>());
            Assert.IsType<List<ToDoViewModel>>(result);
        }

        [Fact]
        public void UpdateToDo_ByDefault_ReturnsToDoViewModel()
        {
            var toDoRepositoryStub = new Mock<IToDoRepository>();
            var mapperStub = new Mock<IMapper>();
            toDoRepositoryStub.Setup
                (e => e.UpdateToDo
                    (It.IsAny<ToDo>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(new ToDo());
            mapperStub.Setup(e => e.Map<ToDoViewModel>(It.IsAny<ToDo>()))
                .Returns(new ToDoViewModel());
            
            var service = new ToDoService
                (toDoRepositoryStub.Object, mapperStub.Object);

            var result = service.UpdateToDo
                (It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>(), It.IsAny<Guid>());
            Assert.IsType<ToDoViewModel>(result);
        }

        [Fact]
        public void DeleteToDo_WhenNotFound_ThrowsResourceNotFoundException()
        {
            var toDoRepositoryStub = new Mock<IToDoRepository>();
            var mapperStub = new Mock<IMapper>();
            toDoRepositoryStub.Setup(e => e.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws<ResourceNotFoundException>();
            
            var service = new ToDoService
                (toDoRepositoryStub.Object, mapperStub.Object);

            Assert.Throws<ResourceNotFoundException>
                (() => service.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()));
        }

        [Fact]
        public void UpdateToDo_WhenNotFound_ThrowsResourceNotFoundException()
        {
            var toDoRepositoryStub = new Mock<IToDoRepository>();
            var mapperStub = new Mock<IMapper>();
            toDoRepositoryStub.Setup
                (e => e.UpdateToDo
                    (It.IsAny<ToDo>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws<ResourceNotFoundException>();
            
            var service = new ToDoService
                (toDoRepositoryStub.Object, mapperStub.Object);

            Assert.Throws<ResourceNotFoundException>
            (() => service.UpdateToDo
                (It.IsAny<ToDoBindingModel>(), 
                It.IsAny<Guid>(), 
                It.IsAny<Guid>()));
        }
    }
}