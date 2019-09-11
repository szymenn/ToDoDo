using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Moq;
using ToDoListApi.Options;
using Xunit;
using ToDoListApi.Services;

namespace ToDoListApi.Tests
{
    public class JwtHandlerTests
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        
        public JwtHandlerTests()
        {
            _jwtSettings = Microsoft.Extensions.Options.Options.Create(new JwtSettings
            {
                Audience = "Audience",
                Issuer = "Issuer",
                AccessExpiration = It.IsAny<int>(),
                RefreshExpiration = It.IsAny<int>(),
                Secret = "secretlk;lkl;kl;k;k;k;kk"
            });
        }
        
        [Fact]
        public void CreateAccessToken_ReturnsTokenString()
        {
            var jwtHandler = new JwtHandler(_jwtSettings);

            var result = jwtHandler.CreateAccessToken(It.IsAny<ICollection<Claim>>());

            Assert.IsType<string>(result);
        }
    }
}