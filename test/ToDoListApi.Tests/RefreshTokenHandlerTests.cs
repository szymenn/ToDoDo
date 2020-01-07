using System;
using Moq;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Repositories;
using ToDoListApi.Services;
using Xunit;

namespace ToDoListApi.Tests
{
    public class RefreshTokenHandlerTests
    {
        private readonly Mock<ITokenRepository> _tokenRepositoryMock;
        private readonly RefreshTokenHandler _refreshTokenHandler;

        public RefreshTokenHandlerTests()
        {
            _tokenRepositoryMock = new Mock<ITokenRepository>();
            
            _refreshTokenHandler = new RefreshTokenHandler(_tokenRepositoryMock.Object);
        }

        [Fact]
        public void CreateRefreshToken_ByDefault_ReturnsTokenString()
        {
            var result = _refreshTokenHandler.CreateRefreshToken(It.IsAny<string>(), It.IsAny<Guid>());

            Assert.IsType<string>(result);
        }

        [Fact]
        public void CreateRefreshToken_ByDefault_CallsTokenRepository()
        {
            var result = _refreshTokenHandler.CreateRefreshToken(It.IsAny<string>(), It.IsAny<Guid>());

            _tokenRepositoryMock.Verify(e => e.SaveRefreshToken(It.IsAny<RefreshToken>()), Times.Once);
        }

        [Fact]
        public void CreateRefreshToken_WhenTokenAlreadyExists_ThrowsResourceAlreadyExistException()
        {
            _tokenRepositoryMock.Setup(e => e.SaveRefreshToken(It.IsAny<RefreshToken>()))
                .Throws<ResourceAlreadyExistsException>();

            Assert.Throws<ResourceAlreadyExistsException>
                (() => _refreshTokenHandler.CreateRefreshToken
                (It.IsAny<string>(), It.IsAny<Guid>()));
        }

        [Fact]
        public void UpdateRefreshToken_ByDefault_ReturnsTokenString()
        {
            _tokenRepositoryMock.Setup
                    (e => e.UpdateRefreshToken(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new string(It.IsAny<string>()));

            var result = _refreshTokenHandler.UpdateRefreshToken(It.IsAny<string>());

            Assert.IsType<string>(result);
        }

        [Fact]
        public void UpdateRefreshToken_ByDefault_CallsTokenRepository()
        {
            var result = _refreshTokenHandler.UpdateRefreshToken(It.IsAny<string>());

            _tokenRepositoryMock.Verify
            (e => e.UpdateRefreshToken
                (It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void UpdateRefreshToken_WhenTokenNotFound_ThrowsResourceNotFoundException()
        {
            _tokenRepositoryMock.Setup
                    (e => e.UpdateRefreshToken(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();

            Assert.Throws<ResourceNotFoundException>
                (() => _refreshTokenHandler.UpdateRefreshToken(It.IsAny<string>()));
        }
    }
}