using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoListApi.Entities;
using ToDoListApi.Models;
using ToDoListApi.Options;
using ToDoListApi.Repositories;

namespace ToDoListApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenHandler _refreshHandler;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IOptions<JwtSettings> jwtSettings, IJwtHandler jwtHandler, IRefreshTokenHandler refreshHandler, ITokenRepository tokenRepository)
        {
            _jwtSettings = jwtSettings;
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
                Expires = _jwtSettings.Value.AccessExpiration
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
                Expires = _jwtSettings.Value.AccessExpiration
            };
        }

        public void RevokeRefreshToken(string token)
        {
            _tokenRepository.RevokeRefreshToken(token);
        }
    }
}