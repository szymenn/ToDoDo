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
        private readonly Mock<IToDoRepository> _toDoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ToDoService _service;

        public ToDoServiceTests()
        {
            _toDoRepositoryMock = new Mock<IToDoRepository>();
            _mapperMock = new Mock<IMapper>();
            
            _service =  new ToDoService
                (_toDoRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetToDos_ByDefault_ReturnsToDoViewModelList()
        {
            _toDoRepositoryMock.Setup(e => e.GetToDos(It.IsAny<Guid>()))
                .Returns(new List<ToDo>());
            _mapperMock.Setup(e => e.Map<ICollection<ToDoViewModel>>(It.IsAny<ICollection<ToDo>>()))
                .Returns(new List<ToDoViewModel>());

            var result = _service.GetToDos(It.IsAny<Guid>());
            
            Assert.IsType<List<ToDoViewModel>>(result);
        }

        [Fact]
        public void AddToDo_ByDefault_ReturnsToDoViewModelList()
        {
            _toDoRepositoryMock.Setup(e => e.AddToDo(It.IsAny<ToDo>(), It.IsAny<Guid>()))
                .Returns(new List<ToDo>());
            _mapperMock.Setup(e => e.Map<ICollection<ToDoViewModel>>(It.IsAny<ICollection<ToDo>>()))
                .Returns(new List<ToDoViewModel>());

            var result = _service.AddToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>());
            
            Assert.IsType<List<ToDoViewModel>>(result);
        }

        [Fact]
        public void DeleteToDo_ByDefault_ReturnsToDoViewModelList()
        {
            _toDoRepositoryMock.Setup(e => e.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(new List<ToDo>());
            _mapperMock.Setup(e => e.Map<ICollection<ToDoViewModel>>(It.IsAny<ICollection<ToDo>>()))
                .Returns(new List<ToDoViewModel>());
            
            var result = _service.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>());
            
            Assert.IsType<List<ToDoViewModel>>(result);
        }

        [Fact]
        public void UpdateToDo_ByDefault_ReturnsToDoViewModel()
        {
            _toDoRepositoryMock.Setup
                (e => e.UpdateToDo
                    (It.IsAny<ToDo>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(new ToDo());
            _mapperMock.Setup(e => e.Map<ToDoViewModel>(It.IsAny<ToDo>()))
                .Returns(new ToDoViewModel());
            
            var result = _service.UpdateToDo
                (It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>(), It.IsAny<Guid>());
            
            Assert.IsType<ToDoViewModel>(result);
        }

        [Fact]
        public void DeleteToDo_WhenNotFound_ThrowsResourceNotFoundException()
        {
            _toDoRepositoryMock.Setup(e => e.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws<ResourceNotFoundException>();

            Assert.Throws<ResourceNotFoundException>
                (() => _service.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()));
        }

        [Fact]
        public void UpdateToDo_WhenNotFound_ThrowsResourceNotFoundException()
        {
            _toDoRepositoryMock.Setup
                (e => e.UpdateToDo
                    (It.IsAny<ToDo>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws<ResourceNotFoundException>();
            
            Assert.Throws<ResourceNotFoundException>
            (() => _service.UpdateToDo
                (It.IsAny<ToDoBindingModel>(), 
                It.IsAny<Guid>(), 
                It.IsAny<Guid>()));
        }

        [Fact]
        public void GetToDos_ByDefault_CallsToDoRepository()
        {
            var result = _service.GetToDos(It.IsAny<Guid>());
            
            _toDoRepositoryMock.Verify(e => e.GetToDos(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void GetToDos_ByDefault_CallsMapper()
        {
            var result = _service.GetToDos(It.IsAny<Guid>());

            _mapperMock.Verify(e => e.Map<ICollection<ToDoViewModel>>
                (It.IsAny<ICollection<ToDo>>()), Times.Once);
        }

        [Fact]
        public void AddToDo_ByDefault_CallsToDoRepository()
        {
            var result = _service.AddToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>());

            _toDoRepositoryMock.Verify
                (e => e.AddToDo(It.IsAny<ToDo>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void AddToDo_ByDefault_CallsMapper()
        {
            var result = _service.AddToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>());
            
            _mapperMock.Verify(e => e.Map<ToDo>(It.IsAny<ToDoBindingModel>()), Times.Once);
            _mapperMock.Verify
                (e => e.Map<ICollection<ToDoViewModel>>(It.IsAny<ICollection<ToDo>>()), Times.Once);
        }

        [Fact]
        public void DeleteToDo_ByDefault_CallsToDoRepository()
        {
            var result = _service.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>());
            
            _toDoRepositoryMock.Verify
                (e => e.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void DeleteToDo_ByDefault_CallsMapper()
        {
            var result = _service.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>());

            _mapperMock.Verify
                (e => e.Map<ICollection<ToDoViewModel>>(It.IsAny<ICollection<ToDo>>()));
        }

        [Fact]
        public void UpdateToDo_ByDefault_CallsToDoRepository()
        {
            var result = _service.UpdateToDo
                (It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>(), It.IsAny<Guid>());

            _toDoRepositoryMock.Verify
                (e => e.UpdateToDo
                (It.IsAny<ToDo>(), It.IsAny<Guid>(), It.IsAny<Guid>()));
        }

        [Fact]
        public void UpdateToDo_ByDefault_CallsMapper()
        {
            var result = _service.UpdateToDo
                (It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>(), It.IsAny<Guid>());
            
            _mapperMock.Verify(e => e.Map<ToDo>(It.IsAny<ToDoBindingModel>()));
            _mapperMock.Verify(e => e.Map<ToDoViewModel>(It.IsAny<ToDo>()));
        }
        
    }
}