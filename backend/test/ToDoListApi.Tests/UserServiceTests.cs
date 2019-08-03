using System;
using System.Threading.Tasks;
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
    public class UserServiceTests
    {
        [Fact]
        public async Task Login_ByDefault_ReturnsStringToken()
        {
            var userRepositoryStub = new Mock<IUserRepository>();
            var mapperStub = new Mock<IMapper>();
            userRepositoryStub.Setup(e => e.Login
                    (It.IsAny<LoginBindingModel>()))
                .Returns(Task.FromResult(new string(It.IsAny<string>())));

            var service = new UserService
                (userRepositoryStub.Object, mapperStub.Object);
            var result = await service.Login(It.IsAny<LoginBindingModel>());
            
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task Register_ByDefault_ReturnsStringToken()
        {
            var userRepositoryStub = new Mock<IUserRepository>();
            var mapperStub = new Mock<IMapper>();
            userRepositoryStub.Setup(e => e.Register
                    (It.IsAny<RegisterBindingModel>()))
                .Returns(Task.FromResult<string>(new string(It.IsAny<string>())));
            
            var service = new UserService
                (userRepositoryStub.Object, mapperStub.Object);
            var result = await service.Register(It.IsAny<RegisterBindingModel>());

            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task Register_WhenAlreadyExists_ThrowsResourceAlreadyExistsException()
        {
            var userRepositoryStub = new Mock<IUserRepository>();
            var mapperStub = new Mock<IMapper>();
            userRepositoryStub.Setup(e => e.Register
                    (It.IsAny<RegisterBindingModel>()))
                .Throws<ResourceAlreadyExistsException>();
            
            var service = new UserService
                (userRepositoryStub.Object, mapperStub.Object);
            var result = service.Register(It.IsAny<RegisterBindingModel>());

            await Assert.ThrowsAsync<ResourceAlreadyExistsException>
                (() => result);
        }

        [Fact]
        public async Task Register_WhenNotSucceeded_ThrowsRegistrationException()
        {
            var userRepositoryStub = new Mock<IUserRepository>();
            var mapperStub = new Mock<IMapper>();
            userRepositoryStub.Setup(e => e.Register
                    (It.IsAny<RegisterBindingModel>()))
                .Throws<RegistrationException>();
            
            var service = new UserService
                (userRepositoryStub.Object, mapperStub.Object);
            var result = service.Register(It.IsAny<RegisterBindingModel>());

            await Assert.ThrowsAsync<RegistrationException>
                (() => result);
        }

        [Fact]
        public void GetUser_ByDefault_ReturnsAppUser()
        {
            var userRepositoryStub = new Mock<IUserRepository>();
            var mapperStub = new Mock<IMapper>();
            userRepositoryStub.Setup(e => e.GetUser(It.IsAny<string>()))
                .Returns(new AppUser());
            mapperStub.Setup(e => e.Map<UserViewModel>(It.IsAny<AppUser>()))
                .Returns(new UserViewModel());
            
            var service = new UserService
                (userRepositoryStub.Object, mapperStub.Object);
            var result = service.GetUser(It.IsAny<string>());

            Assert.IsType<UserViewModel>(result);
        }

        [Fact]
        public void GetUser_WhenNotFound_ThrowsResourceNotFoundException()
        {
            var userRepositoryStub = new Mock<IUserRepository>();
            var mapperStub = new Mock<IMapper>();
            userRepositoryStub.Setup(e => e.GetUser(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();
            
            var service = new UserService
                (userRepositoryStub.Object, mapperStub.Object);

            Assert.Throws<ResourceNotFoundException>
                (() => service.GetUser(It.IsAny<string>()));
        }
    }
}