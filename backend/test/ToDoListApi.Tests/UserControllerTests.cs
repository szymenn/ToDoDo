using System;
using System.Security.Claims;
using System.Threading.Tasks;
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
        private readonly UserController _controller;
        private readonly Mock<IUserService> _userServiceMock;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            }));
            _controller = new UserController
                (_userServiceMock.Object)
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
        public async Task Login_ByDefault_ReturnsOkObjectResult()
        {
            _userServiceMock.Setup(e => e.Login(It.IsAny<LoginBindingModel>()))
                .Returns(Task.FromResult(new JsonWebToken()));

            var result = await _controller.Login(It.IsAny<LoginBindingModel>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Register_ByDefault_ReturnsOkObjectResult()
        {
            _userServiceMock.Setup(e => e.Register(It.IsAny<RegisterBindingModel>()))
                .Returns(Task.FromResult(new EmailResponse()));
            
            var result = await _controller.Register(It.IsAny<RegisterBindingModel>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetUser_ByDefault_ReturnsOkObjectResult()
        {
            _userServiceMock.Setup(e => e.GetUser(It.IsAny<string>()))
                .Returns(new UserViewModel());
            
            var result = _controller.GetUser();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Login_WhenNotFound_ThrowsResourceNotFoundException()
        {
            _userServiceMock.Setup(e => e.Login(It.IsAny<LoginBindingModel>()))
                .Throws<ResourceNotFoundException>();
            
            var result = _controller.Login(It.IsAny<LoginBindingModel>());

            await Assert.ThrowsAsync<ResourceNotFoundException>(() => result);
        }

        [Fact]
        public async Task Login_WhenIncorrectPassword_ThrowsPasswordValidationException()
        {
            _userServiceMock.Setup(e => e.Login(It.IsAny<LoginBindingModel>()))
                .Throws<LoginException>();
            
            var result = _controller.Login(It.IsAny<LoginBindingModel>());

            await Assert.ThrowsAsync<LoginException>(() => result);
        }

        [Fact]
        public async Task Register_WhenAlreadyExists_ThrowsResourceAlreadyExistsException()
        {
            _userServiceMock.Setup(e => e.Register(It.IsAny<RegisterBindingModel>()))
                .Throws<ResourceAlreadyExistsException>();
            
            var result = _controller.Register(It.IsAny<RegisterBindingModel>());

            await Assert.ThrowsAsync<ResourceAlreadyExistsException>(() => result);
        }

        [Fact]
        public async Task Register_WhenErrorOccured_ThrowsRegistrationException()
        {
            _userServiceMock.Setup(e => e.Register(It.IsAny<RegisterBindingModel>()))
                .Throws<RegistrationException>();
            
            var result = _controller.Register(It.IsAny<RegisterBindingModel>());

            await Assert.ThrowsAsync<RegistrationException>(() => result);
        }
        
        [Fact]
        public void GetUser_WhenUserNotFound_ThrowsResourceNotFoundException()
        {
            _userServiceMock.Setup(e => e.GetUser(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();

            Assert.Throws<ResourceNotFoundException>
                (() => _controller.GetUser());
        }

        [Fact]
        public void RefreshAccessToken_ByDefault_ReturnsOkObjectResult()
        {
            _userServiceMock.Setup(e => e.RefreshAccessToken(It.IsAny<string>()))
                .Returns(new JsonWebToken());
            
            var result = _controller.RefreshAccessToken(It.IsAny<string>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RefreshAccessToken_WhenTokenNotFound_ThrowsResourceNotFoundException()
        {
            _userServiceMock.Setup(e => e.RefreshAccessToken(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();
            
            Assert.Throws<ResourceNotFoundException>
                (() => _controller.RefreshAccessToken(It.IsAny<string>()));
        }

        [Fact]
        public void RevokeRefreshToken_ByDefault_ReturnsNoContentResult()
        {
            var result = _controller.RevokeRefreshToken(It.IsAny<string>());

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void RevokeRefreshToken_ByDefault_CallsUserService()
        {
            var result = _controller.RevokeRefreshToken(It.IsAny<string>());
            
            _userServiceMock.Verify(e => e.RevokeRefreshToken(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void RevokeRefreshToken_WhenTokenNotFound_ThrowsResourceNotFoundException()
        {
            _userServiceMock.Setup(e => e.RevokeRefreshToken(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();
            
            Assert.Throws<ResourceNotFoundException>
                (() => _controller.RevokeRefreshToken(It.IsAny<string>()));
        }

        [Fact]
        public async Task Login_ByDefault_CallsUserService()
        {
            var result = await _controller.Login(It.IsAny<LoginBindingModel>());

            _userServiceMock.Verify(e => e.Login(It.IsAny<LoginBindingModel>()), Times.Once);
        }

        [Fact]
        public async Task Register_ByDefault_CallsUserService()
        {
            var result = await _controller.Register(It.IsAny<RegisterBindingModel>());
            
            _userServiceMock.Verify(e => e.Register(It.IsAny<RegisterBindingModel>()));
        }

        [Fact]
        public void GetUser_ByDefault_CallsUserService()
        {
            var result = _controller.GetUser();
            
            _userServiceMock.Verify(e => e.GetUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void RefreshAccessToken_ByDefault_CallsUserService()
        {
            var result = _controller.RefreshAccessToken(It.IsAny<string>());
            
            _userServiceMock.Verify(e => e.RefreshAccessToken(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerifyEmail_ByDefault_CallsUserService()
        {
            var result = _controller.VerifyEmail(It.IsAny<string>(), It.IsAny<string>());

            _userServiceMock.Verify
                (e => e.VerifyEmail(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}