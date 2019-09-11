using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Moq;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;
using ToDoListApi.Repositories;
using Xunit;

namespace ToDoListApi.Tests
{
    public class TokenRepositoryTests : TokenRepositoryTestBase
    {
        private readonly TokenRepository _tokenRepository;

        private const string UpdateToken = "updated token";
        
        public TokenRepositoryTests()
        {
            _tokenRepository = new TokenRepository(Context);
        }
        
        [Fact]
        public void SaveRefreshToken_ByDefault_SavesToken()
        {
            var refreshToken = new RefreshToken
            {
                Id = It.IsAny<Guid>(),
                Token = It.IsAny<string>(),
                Revoked = false,
                UserName = It.IsAny<string>(),
                UserId = It.IsAny<Guid>()
            };
            
            _tokenRepository.SaveRefreshToken(refreshToken);
            
            Assert.Equal( 3, Context.Tokens.Local.Count);
        }

        [Fact]
        public void SaveRefreshToken_WhenAlreadyExists_ThrowsResourceAlreadyExistException()
        {
            var refreshToken = new RefreshToken
            {
                Id = Guid.Parse("4efb956c-a645-4480-9ed9-abc49acb55e0"),
                Token = Token,
                Revoked = false,
                UserName = "username",
                UserId = Guid.Parse("e2875b2c-faa6-4f40-8175-f2c6e565eb9a")
            };

            Assert.Throws<ResourceAlreadyExistsException>
                (() => _tokenRepository.SaveRefreshToken(refreshToken));
        }

        [Fact]
        public void RemoveRefreshToken_ByDefault_RemovesRefreshToken()
        {
            _tokenRepository.RemoveRefreshToken(Token);
            
            Assert.Single(Context.Tokens.Local);
        }


        [Fact]
        public void RemoveRefreshToken_WhenNotFound_ThrowsResourceNotFoundException()
        {
            Assert.Throws<ResourceNotFoundException>
            (() => _tokenRepository.RemoveRefreshToken(It.Is<string>(m =>
                !m.Equals(Token) && !m.Equals(DifferentToken))));
        }

        [Fact]
        public void CanRefresh_WhenNotRevoked_ReturnsTrue()
        {
            var result = _tokenRepository.CanRefresh(Token);
            
            Assert.True(result);
        }

        [Fact]
        public void CanRefresh_WhenRevoke_ReturnsFalse()
        {
            var result = _tokenRepository.CanRefresh(DifferentToken);
            
            Assert.False(result);
        }

        [Fact]
        public void CanRefresh_WhenNotFound_ReturnsFalse()
        {
            var result = _tokenRepository.CanRefresh
                    (It.Is<string>(m => !m.Equals(Token) && !m.Equals(DifferentToken)));
            
            Assert.False(result);
        }

        [Fact]
        public void RevokeRefreshToken_ByDefault_RevokesRefreshToken()
        {
            _tokenRepository.RevokeRefreshToken(Token);

            var revokedToken = Context.Tokens.First(p => p.Token == Token);
            
            Assert.True(revokedToken.Revoked);
        }

        [Fact]
        public void RevokeRefreshToken_WhenNotFound_ThrowsResourceNotFoundException()
        {
            Assert.Throws<ResourceNotFoundException>
            (() => _tokenRepository.RevokeRefreshToken(It.Is<string>(m =>
                !m.Equals(Token) && !m.Equals(DifferentToken))));
        }

        [Fact]
        public void UpdateRefreshToken_ByDefault_UpdatesToken()
        {
            var result = _tokenRepository.UpdateRefreshToken(Token, UpdateToken);

            Assert.Equal(UpdateToken, result);
        }

        [Fact]
        public void UpdateRefreshToken_ByDefault_ReturnsTokenString()
        {
            var result = _tokenRepository.UpdateRefreshToken(Token, UpdateToken);

            Assert.IsType<string>(result);
        }

        [Fact]
        public void UpdateRefreshToken_WhenNotFound_ThrowsResourceNotFoundException()
        {
            Assert.Throws<ResourceNotFoundException>
            (() => _tokenRepository.UpdateRefreshToken(It.Is<string>(m =>
                !m.Equals(Token) && !m.Equals(DifferentToken)), UpdateToken));
        }

        [Fact]
        public void GetUserClaims_ByDefault_ReturnsClaimsCollection()
        {
            var result = _tokenRepository.GetUserClaims(Token);

            Assert.IsAssignableFrom<ICollection<Claim>>(result);
        }

        [Fact]
        public void GetUserClaims_WhenNotFound_ThrowsResourceNotFoundException()
        {
            Assert.Throws<ResourceNotFoundException>
            (() => _tokenRepository.GetUserClaims(It.Is<string>(m =>
                !m.Equals(Token) && !m.Equals(DifferentToken))));
        }
    }
}