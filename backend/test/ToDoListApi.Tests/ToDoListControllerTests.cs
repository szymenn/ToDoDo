using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoListApi.Controllers;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Models;
using ToDoListApi.Services;
using Xunit;

namespace ToDoListApi.Tests
{
    public class ToDoListControllerTests
    {
        [Fact]
        public void GetToDos_ByDefault_ReturnsOkObjectResult()
        {
            var mapperStub = new Mock<IMapper>();
            var toDoServiceStub = new Mock<IToDoService>();
            toDoServiceStub.Setup(e => e.GetToDos(It.IsAny<Guid>()))
                .Returns(new List<ToDo>());

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }));

            var controller = new ToDoListController
                (toDoServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext
                        {
                            User = user
                        }
                    }
                };

            var result = controller.GetToDos();   
            
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void AddToDo_ByDefault_ReturnsOkObjectResult()
        {
            var mapperStub = new Mock<IMapper>();
            var toDoServiceStub = new Mock<IToDoService>();
            toDoServiceStub.Setup(e => e.AddToDo(It.IsAny<ToDo>(), It.IsAny<Guid>()))
                .Returns(new List<ToDo>());
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }));

            var controller = new ToDoListController
                (toDoServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext
                        {
                            User = user
                        }
                    }
                };

            var result = controller.AddToDo(It.IsAny<ToDoBindingModel>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteToDo_ByDefault_ReturnsOkObjectResult()
        {
            var mapperStub = new Mock<IMapper>();
            var toDoServiceStub = new Mock<IToDoService>();
            toDoServiceStub.Setup(e => e.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(new List<ToDo>());
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }));

            var controller = new ToDoListController
                (toDoServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext
                        {
                            User = user
                        }
                    }
                };

            var result = controller.DeleteToDo(It.IsAny<Guid>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateToDo_ByDefault_ReturnsOkObjectResult()
        {
            var mapperStub = new Mock<IMapper>();
            var toDoServiceStub = new Mock<IToDoService>();
            toDoServiceStub.Setup(e => e.UpdateToDo
                (It.IsAny<ToDo>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()))
                .Returns(new ToDo());
            
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }));

            var controller = new ToDoListController
                (toDoServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext
                        {
                            User = user
                        }
                    }
                };

            var result = controller.UpdateToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteToDo_WhenNotFound_ThrowsException()
        {
            var mapperStub = new Mock<IMapper>();
            var toDoServiceStub = new Mock<IToDoService>();
            toDoServiceStub.Setup(e => e.DeleteToDo(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws<ResourceNotFoundException>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }));

            var controller = new ToDoListController
                (toDoServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext
                        {
                            User = user
                        }
                    }
                };

            Assert.Throws<ResourceNotFoundException>(() => controller.DeleteToDo(It.IsAny<Guid>()));
        }

        [Fact]
        public void UpdateToDo_WhenNotFound_ThrowsException()
        {
            var mapperStub = new Mock<IMapper>();
            var toDoServiceStub = new Mock<IToDoService>();
            toDoServiceStub.Setup(e => e.UpdateToDo
                    (It.IsAny<ToDo>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Throws<ResourceNotFoundException>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }));

            var controller = new ToDoListController
                (toDoServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext
                        {
                            User = user
                        }
                    }
                };

            Assert.Throws<ResourceNotFoundException>
                (() => controller.UpdateToDo(It.IsAny<ToDoBindingModel>(), It.IsAny<Guid>()));
        }
    }    
}