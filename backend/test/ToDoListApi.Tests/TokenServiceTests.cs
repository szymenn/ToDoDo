using System;
using System.Collections.Generic;
using System.Security.Claims;
using Moq;
using ToDoListApi.Exceptions;
using ToDoListApi.Models;
using ToDoListApi.Repositories;
using ToDoListApi.Services;
using Xunit;

namespace ToDoListApi.Tests
{
    public class TokenServiceTests
    {
        private readonly Mock<IJwtHandler> _jwtHandlerMock;
        private readonly Mock<IRefreshTokenHandler> _refreshHandlerMock;
        private readonly Mock<ITokenRepository> _tokenRepositoryMock;
        private readonly TokenService _tokenService;
        private const string Username = "username";
        
        public TokenServiceTests()
        {
            _jwtHandlerMock = new Mock<IJwtHandler>();
            _refreshHandlerMock = new Mock<IRefreshTokenHandler>();
            _tokenRepositoryMock = new Mock<ITokenRepository>();

            _tokenService = new TokenService(_jwtHandlerMock.Object, _refreshHandlerMock.Object,
                _tokenRepositoryMock.Object);
        }
        
        [Fact]
        public void CreateAccessToken_ByDefault_ReturnsJsonWebToken()
        {
            _jwtHandlerMock.Setup(e => e.CreateAccessToken(It.IsAny<ICollection<Claim>>()))
                .Returns(It.IsAny<string>());
            _refreshHandlerMock.Setup(e => e.CreateRefreshToken
                    (It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(It.IsAny<string>());

            var result = _tokenService.CreateAccessToken(Username, It.IsAny<Guid>());

            Assert.IsType<JsonWebToken>(result);
        }

        [Fact]
        public void CreateAccessToken_ByDefault_CallsJwtHandler()
        {
            var result = _tokenService.CreateAccessToken(Username, It.IsAny<Guid>());

            _jwtHandlerMock.Verify
                (e => e.CreateAccessToken(It.IsAny<ICollection<Claim>>()), Times.Once);
        }

        [Fact]
        public void CreateAccessToken_ByDefault_CallsRefreshHandler()
        {
            var result = _tokenService.CreateAccessToken(Username, It.IsAny<Guid>());

            _refreshHandlerMock.Verify
            (e => e.CreateRefreshToken
                (It.IsAny<string>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public void CreateAccessToken_WhenRefreshTokenAlreadyExists_ThrowsResourceAlreadyExistsException()
        {
            _refreshHandlerMock.Setup
                    (e => e.CreateRefreshToken(It.IsAny<string>(), It.IsAny<Guid>()))
                .Throws<ResourceAlreadyExistsException>();

            Assert.Throws<ResourceAlreadyExistsException>
                (() => _tokenService.CreateAccessToken(Username, It.IsAny<Guid>()));
        }

        [Fact]
        public void RefreshAccessToken_ByDefault_ReturnsJsonWebToken()
        {
            _tokenRepositoryMock.Setup(e => e.GetUserClaims(It.IsAny<string>()))
                .Returns(It.IsAny<ICollection<Claim>>());
            
            var result = _tokenService.RefreshAccessToken(It.IsAny<string>());

            Assert.IsType<JsonWebToken>(result);
        }

        [Fact]
        public void RefreshAccessToken_ByDefault_CallsJwtHandler()
        {
            var result = _tokenService.RefreshAccessToken(It.IsAny<string>());
            
            _jwtHandlerMock.Verify
                (e => e.CreateAccessToken(It.IsAny<ICollection<Claim>>()), Times.Once);
        }

        [Fact]
        public void RefreshAccessToken_ByDefault_CallsRefreshTokenHandler()
        {
            var result = _tokenService.RefreshAccessToken(It.IsAny<string>());
            
            _refreshHandlerMock.Verify
                (e => e.UpdateRefreshToken(It.IsAny<string>()));
        }

        [Fact]
        public void RefreshAccessToken_ByDefault_CallsTokenRepository()
        {
            var result = _tokenService.RefreshAccessToken(It.IsAny<string>());
            
            _tokenRepositoryMock.Verify(e => e.GetUserClaims(It.IsAny<string>()), Times.Once);
        }
        
        [Fact]
        public void RefreshAccessToken_WhenTokenNotFound_ThrowsResourceNotFoundException()
        {
            _refreshHandlerMock.Setup(e => e.UpdateRefreshToken(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();

            Assert.Throws<ResourceNotFoundException>
                (() => _tokenService.RefreshAccessToken(It.IsAny<string>()));
        }

        [Fact]
        public void RevokeRefreshToken_ByDefault_CallsTokenRepository()
        {
            _tokenService.RevokeRefreshToken(It.IsAny<string>());
            
            _tokenRepositoryMock.Verify(e => e.RevokeRefreshToken(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void RevokeRefreshToken_WhenTokenNotFound_ThrowsResourceNotFoundException()
        {
            _tokenRepositoryMock.Setup(e => e.RevokeRefreshToken(It.IsAny<string>()))
                .Throws<ResourceNotFoundException>();

            Assert.Throws<ResourceNotFoundException>
                (() => _tokenService.RevokeRefreshToken(It.IsAny<string>()));
        }
    }
}