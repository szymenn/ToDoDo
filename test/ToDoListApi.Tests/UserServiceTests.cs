using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using ToDoListApi.Email;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Models;
using ToDoListApi.Repositories;
using ToDoListApi.Services;
using Xunit;

namespace ToDoListApi.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly UserService _service;
        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _tokenServiceMock = new Mock<ITokenService>();
            
            _service = new UserService
                (_userRepositoryMock.Object, _mapperMock.Object, _tokenServiceMock.Object);
        }
        
        [Fact]
        public async Task Login_ByDefault_ReturnsJsonWebToken()
        {
            _userRepositoryMock.Setup(e => e.Login
                    (It.IsAny<LoginBindingModel>()))
                .Returns(Task.FromResult(new JsonWebToken()));

            var result = await _service.Login(It.IsAny<LoginBindingModel>());
            
            Assert.IsType<JsonWebToken>(result);
        }

        [Fact]
        public async Task Login_WhenUserNotFound_ThrowsResourceNotFoundException()
        {
            _userRepositoryMock.Setup(e => e.Login(It.IsAny<LoginBindingModel>()))
                .Throws<ResourceNotFoundException>();

            await Assert.ThrowsAsync<ResourceNotFoundException>
                (() => _service.Login(It.IsAny<LoginBindingModel>()));
        }

        [Fact]
        public async Task Login_WhenLoginFailed_ThrowsLoginException()
        {
            _userRepositoryMock.Setup(e => e.Login(It.IsAny<LoginBindingModel>()))
                .Throws<LoginException>();

            await Assert.ThrowsAsync<LoginException>
                (() => _service.Login(It.IsAny<LoginBindingModel>()));
        }

        [Fact]
        public async Task Register_ByDefault_ReturnsEmailResponse()
        {
            _userRepositoryMock.Setup(e => e.Register
                    (It.IsAny<RegisterBindingModel>()))
                .Returns(Task.FromResult(new EmailResponse()));
            
            var result = await _service.Register(It.IsAny<RegisterBindingModel>());

            Assert.IsType<EmailResponse>(result);
        }

        [Fact]
        public async Task Register_WhenAlreadyExists_ThrowsResourceAlreadyExistsException()
        {
            _userRepositoryMock.Setup(e => e.Register
                    (It.IsAny<RegisterBindingModel>()))
                .Throws<ResourceAlreadyExistsException>();
            
            var result = _service.Register(It.IsAny<RegisterBindingModel>());

            await Assert.ThrowsAsync<ResourceAlreadyExistsException>
                (() => result);
        }

        [Fact]
        public async Task Register_WhenNotSucceeded_ThrowsRegistrationException()
        {
            _userRepositoryMock.Setup(e => e.Register
                    (It.IsAny<RegisterBindingModel>()))
                .Throws<RegistrationException>();
            
            var result = _service.Register(It.IsAny<RegisterBindingModel>());

            await Assert.ThrowsAsync<RegistrationException>
                (() => result);
        }

        [Fact]
        public void GetUser_ByDefault_ReturnsAppUser()
        {
            _userRepositoryMock.Setup(e => e.GetUser(It.IsAny<string>()))
                .Returns(new AppUser());
            _mapperMock.Setup(e => e.Map<UserViewModel>(It.IsAny<AppUser>()))
                .Returns(new UserViewModel());
            
            var result = _service.GetUser(It.IsAny<string>());

            Assert.IsType<UserViewModel>(result);
        }

        [Fact]
        public void GetUser_WhenNotFound_ThrowsResourceNotFoundException()
        {
            _userRepositoryMock.Setup(e => e.GetUser(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();
            
            Assert.Throws<ResourceNotFoundException>
                (() => _service.GetUser(It.IsAny<string>()));
        }

        [Fact]
        public void RefreshAccessToken_ByDefault_ReturnsJsonWebToken()
        {
            _tokenServiceMock.Setup(e => e.RefreshAccessToken(It.IsAny<string>()))
                .Returns(new JsonWebToken());
            
            var result = _service.RefreshAccessToken(It.IsAny<string>());

            Assert.IsType<JsonWebToken>(result);
        }

        [Fact]
        public void RefreshAccessToken_WhenTokenNotFound_ThrowsResourceNotFoundException()
        {
            _tokenServiceMock.Setup(e => e.RefreshAccessToken(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();
            
            Assert.Throws<ResourceNotFoundException>
                (() => _service.RefreshAccessToken(It.IsAny<string>()));
        }

        [Fact]
        public void RevokeAccessToken_ByDefault_CallsTokenService()
        {
            _service.RevokeRefreshToken(It.IsAny<string>());
            
            _tokenServiceMock.Verify(e => e.RevokeRefreshToken(It.IsAny<string>()), Times.Once);
        }
        
        [Fact]
        public void RevokeAccessToken_WhenNotFound_ThrowsResourceNotFoundException()
        {
            _tokenServiceMock.Setup(e => e.RevokeRefreshToken(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();
            
            Assert.Throws<ResourceNotFoundException>
                (() => _service.RevokeRefreshToken(It.IsAny<string>()));
        }

        [Fact]
        public async Task Login_ByDefault_CallsUserRepository()
        {
            var result = await _service.Login(It.IsAny<LoginBindingModel>());
            
            _userRepositoryMock.Verify(e => e.Login(It.IsAny<LoginBindingModel>()), Times.Once);
        }

        [Fact]
        public async Task Register_ByDefault_CallsUserRepository()
        {
            var result = await _service.Register(It.IsAny<RegisterBindingModel>());
            
            _userRepositoryMock.Verify
                (e => e.Register(It.IsAny<RegisterBindingModel>()), Times.Once);
        }

        [Fact]
        public async Task VerifyEmail_ByDefault_CallsUserRepository()
        {
            await _service.VerifyEmail(It.IsAny<string>(), It.IsAny<string>());
            
            _userRepositoryMock.Verify
                (e => e.VerifyEmail
                (It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task VerifyEmail_WhenUserNotFound_ThrowsResourceNotFoundException()
        {
            _userRepositoryMock.Setup
                    (e => e.VerifyEmail(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();
            
            await Assert.ThrowsAsync<ResourceNotFoundException>
                (() => _service.VerifyEmail(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task VerifyEmail_WhenVerificationFail_ThrowsEmailVerificationException()
        {
            _userRepositoryMock.Setup
                    (e => e.VerifyEmail(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<EmailVerificationException>();

            await Assert.ThrowsAsync<EmailVerificationException>
                (() => _service.VerifyEmail(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public void GetUser_ByDefault_CallsUserRepository()
        {
            var result = _service.GetUser(It.IsAny<string>());
            
            _userRepositoryMock.Verify(e => e.GetUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetUser_ByDefault_CallsMapper()
        {
            var result = _service.GetUser(It.IsAny<string>());
            
            _mapperMock.Verify(e => e.Map<UserViewModel>(It.IsAny<AppUser>()));
        }

        [Fact]
        public void RefreshAccessToken_ByDefault_CallsTokenService()
        {
            var result = _service.RefreshAccessToken(It.IsAny<string>());
            
            _tokenServiceMock.Verify(e => e.RefreshAccessToken(It.IsAny<string>()));
        }
        
    }
    
}