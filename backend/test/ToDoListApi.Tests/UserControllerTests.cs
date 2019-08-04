using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
    public class UserControllerTests
    {
        [Fact]
        public async Task Login_ByDefault_ReturnsOkObjectResult()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.Login(It.IsAny<LoginBindingModel>()))
                .Returns(Task.FromResult(new string(It.IsAny<string>())));

            var controller = new UserController(userServiceStub.Object);
            var result = await controller.Login(It.IsAny<LoginBindingModel>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Register_ByDefault_ReturnsOkObjectResult()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.Register(It.IsAny<RegisterBindingModel>()))
                .Returns(Task.FromResult(new string(It.IsAny<string>())));
            
            var controller = new UserController(userServiceStub.Object);
            var result = await controller.Register(It.IsAny<RegisterBindingModel>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetUser_ByDefault_ReturnsOkObjectResult()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.GetUser(It.IsAny<string>()))
                .Returns(new UserViewModel());
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }));

            var controller = new UserController
                (userServiceStub.Object)
                {
                    ControllerContext = new ControllerContext
                    {
                        HttpContext = new DefaultHttpContext
                        {
                            User = user
                        }
                    }
                };
            var result = controller.GetUser();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Login_WhenNotFound_ThrowsResourceNotFoundException()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.Login(It.IsAny<LoginBindingModel>()))
                .Throws<ResourceNotFoundException>();
            
            var controller = new UserController(userServiceStub.Object);
            var result = controller.Login(It.IsAny<LoginBindingModel>());

            await Assert.ThrowsAsync<ResourceNotFoundException>(() => result);
        }

        [Fact]
        public async Task Login_WhenIncorrectPassword_ThrowsPasswordValidationException()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.Login(It.IsAny<LoginBindingModel>()))
                .Throws<PasswordValidationException>();
            
            var controller = new UserController(userServiceStub.Object);
            var result = controller.Login(It.IsAny<LoginBindingModel>());

            await Assert.ThrowsAsync<PasswordValidationException>(() => result);
        }

        [Fact]
        public async Task Register_WhenAlreadyExists_ThrowsResourceAlreadyExistsException()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.Register(It.IsAny<RegisterBindingModel>()))
                .Throws<ResourceAlreadyExistsException>();
            
            var controller = new UserController(userServiceStub.Object);
            var result = controller.Register(It.IsAny<RegisterBindingModel>());

            await Assert.ThrowsAsync<ResourceAlreadyExistsException>(() => result);
        }

        [Fact]
        public async Task Register_WhenErrorOccured_ThrowsRegistrationException()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.Register(It.IsAny<RegisterBindingModel>()))
                .Throws<RegistrationException>();
            
            var controller = new UserController(userServiceStub.Object);
            var result = controller.Register(It.IsAny<RegisterBindingModel>());

            await Assert.ThrowsAsync<RegistrationException>(() => result);
        }
        
        [Fact]
        public void GetUser_WhenUserNotFound_ThrowsResourceNotFoundException()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.GetUser(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }));

            var controller = new UserController
                (userServiceStub.Object)
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
                (() => controller.GetUser());
        }
        
    }
}