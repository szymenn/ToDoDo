using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoListApi.Models;
using ToDoListApi.Repositories;

namespace ToDoListApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenHandler _refreshHandler;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(
            IJwtHandler jwtHandler, 
            IRefreshTokenHandler refreshHandler, 
            ITokenRepository tokenRepository
            )
        {
            _jwtHandler = jwtHandler;
            _refreshHandler = refreshHandler;
            _tokenRepository = tokenRepository;
        }

        public JsonWebToken CreateAccessToken(string userName, Guid userId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            return new JsonWebToken
            {
                AccessToken = _jwtHandler.CreateAccessToken(claims),
                RefreshToken = _refreshHandler.CreateRefreshToken(userName, userId),
            };
        }

        public JsonWebToken RefreshAccessToken(string token)
        {
            var userClaims = _tokenRepository.GetUserClaims(token);

            var refreshToken = _refreshHandler.UpdateRefreshToken(token);
            var accessToken = _jwtHandler.CreateAccessToken(userClaims);
            return new JsonWebToken
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public void RevokeRefreshToken(string token)
        {
            _tokenRepository.RevokeRefreshToken(token);
        }
    }
}