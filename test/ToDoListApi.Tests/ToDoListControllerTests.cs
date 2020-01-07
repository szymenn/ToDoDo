using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoListApi.Controllers;
using ToDoListApi.Exceptions;
using ToDoListApi.Models;
using ToDoListApi.Services;
using Xunit;

namespace ToDoListApi.Tests
{
    public class ToDoListControllerTests
    {
        private readonly ToDoListController _controller;
        private readonly Mock<IToDoService> _toDoServiceMock;
        public ToDoListControllerTests()
        {
            _toDoServiceMock = new Mock<IToDoService>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }));
            
            _controller = new ToDoListController
                (_toDoServiceMock.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext
                        {
                            User = user
                        }
                    }
                };
        }
        
        [Fact]
        public void GetToDos_ByDefault_ReturnsOkObjectResult()
        {
            _toDoServiceMock.Setup(e => e.GetToDos(It.IsAny<Guid>()))
                .Returns(new List<ToDoViewModel>());

            var result = _controller.GetToDos();   
            
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void AddToDo_ByDefault_ReturnsOkObjectResult()
        {
            _toDoServiceMock.Setup(e => e.AddToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>()))
                .Returns(new List<ToDoViewModel>());
            
            var result = _controller.AddToDo(It.IsAny<ToDoBindingModel>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteToDo_ByDefault_ReturnsOkObjectResult()
        {
            _toDoServiceMock.Setup(e => e.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(new List<ToDoViewModel>());
            
            var result = _controller.DeleteToDo(It.IsAny<Guid>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateToDo_ByDefault_ReturnsOkObjectResult()
        {
            _toDoServiceMock.Setup(e => e.UpdateToDo
                (It.IsAny<ToDoBindingModel>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()))
                .Returns(new ToDoViewModel());
            

            var result = _controller.UpdateToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteToDo_WhenNotFound_ThrowsException()
        {
            _toDoServiceMock.Setup(e => e.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws<ResourceNotFoundException>();
            
            Assert.Throws<ResourceNotFoundException>(() => _controller.DeleteToDo(It.IsAny<Guid>()));
        }

        [Fact]
        public void UpdateToDo_WhenNotFound_ThrowsException()
        {
            _toDoServiceMock.Setup(e => e.UpdateToDo
                    (It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws<ResourceNotFoundException>();
            
            Assert.Throws<ResourceNotFoundException>
                (() => _controller.UpdateToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>()));
        }

        [Fact]
        public void GetToDos_ByDefault_CallsToDoService()
        {
            var result = _controller.GetToDos();
            
            _toDoServiceMock.Verify(e => e.GetToDos(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void AddToDo_ByDefault_CallsToDoService()
        {
            var result = _controller.AddToDo(It.IsAny<ToDoBindingModel>());
            
            _toDoServiceMock.Verify(e => e.AddToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>()));
        }

        [Fact]
        public void DeleteToDo_ByDefault_CallsToDoService()
        {
            var result = _controller.DeleteToDo(It.IsAny<Guid>());
            
            _toDoServiceMock.Verify
                (e => e.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void UpdateToDo_ByDefault_CallsToDoService()
        {
            var result = _controller.UpdateToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>());

            _toDoServiceMock.Verify(e =>
                e.UpdateToDo
                (It.IsAny<ToDoBindingModel>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()), Times.Once);
        }
    }    
}