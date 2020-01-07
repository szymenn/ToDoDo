using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using ToDoListApi.Entities;
using ToDoListApi.Repositories;

namespace ToDoListApi.Services
{
    public class RefreshTokenHandler : IRefreshTokenHandler
    {
        private readonly ITokenRepository _tokenRepository;

        public RefreshTokenHandler(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }
        
        public string CreateRefreshToken(string userName, Guid userId)
        {
            var refreshToken = CreateRefreshToken();
            SaveRefreshToken(refreshToken, userName, userId);
            return refreshToken;
        }

        public string UpdateRefreshToken(string token)
        {
            var updateToken = CreateRefreshToken();
            return _tokenRepository.UpdateRefreshToken(token, updateToken);
        }
        
        private void SaveRefreshToken(string token, string userName, Guid userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = token,
                UserName = userName,
                UserId = userId
            };
            _tokenRepository.SaveRefreshToken(refreshToken);
        }
        
        private string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var randomGenerator = RandomNumberGenerator.Create())
            {
                randomGenerator.GetBytes(randomNumber);
                var refreshToken = Convert.ToBase64String(randomNumber);
                return refreshToken;
            }
        }
    }
}