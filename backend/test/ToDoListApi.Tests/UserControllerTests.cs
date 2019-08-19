using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoListApi.Controllers;
using ToDoListApi.Email;
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
                .Returns(Task.FromResult(new JsonWebToken()));

            var controller = new UserController(userServiceStub.Object);
            var result = await controller.Login(It.IsAny<LoginBindingModel>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Register_ByDefault_ReturnsOkObjectResult()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.Register(It.IsAny<RegisterBindingModel>()))
                .Returns(Task.FromResult(new EmailResponse()));
            
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
                .Throws<LoginException>();
            
            var controller = new UserController(userServiceStub.Object);
            var result = controller.Login(It.IsAny<LoginBindingModel>());

            await Assert.ThrowsAsync<LoginException>(() => result);
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

        [Fact]
        public void RefreshAccessToken_ByDefault_ReturnsOkObjectResult()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.RefreshAccessToken(It.IsAny<string>()))
                .Returns(new JsonWebToken());
            
            var controller = new UserController(userServiceStub.Object);
            var result = controller.RefreshAccessToken(It.IsAny<string>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RefreshAccessToken_WhenTokenNotFound_ThrowsResourceNotFoundException()
        {
            var userServiceStub  = new Mock<IUserService>();
            userServiceStub.Setup(e => e.RefreshAccessToken(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();
            
            var controller = new UserController(userServiceStub.Object);

            Assert.Throws<ResourceNotFoundException>
                (() => controller.RefreshAccessToken(It.IsAny<string>()));
        }

        [Fact]
        public void RevokeRefreshToken_ByDefault_ReturnsNoContentResult()
        {
            var userServiceStub = new Mock<IUserService>();
            
            var controller = new UserController(userServiceStub.Object);
            var result = controller.RevokeRefreshToken(It.IsAny<string>());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void RevokeRefreshToken_ByDefault_CallsUserService()
        {
            var userServiceStub = new Mock<IUserService>();
            
            var controller = new UserController(userServiceStub.Object);
            var result = controller.RevokeRefreshToken(It.IsAny<string>());
            
            userServiceStub.Verify(e => e.RevokeRefreshToken(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void RevokeRefreshToken_WhenTokenNotFound_ThrowsResourceNotFoundException()
        {
            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(e => e.RevokeRefreshToken(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();
            
            var controller = new UserController(userServiceStub.Object);

            Assert.Throws<ResourceNotFoundException>
                (() => controller.RevokeRefreshToken(It.IsAny<string>()));
        }
        
        
    }
}