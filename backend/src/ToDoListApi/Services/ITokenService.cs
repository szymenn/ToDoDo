using System;
using ToDoListApi.Models;

namespace ToDoListApi.Services
{
    public interface ITokenService
    {
        JsonWebToken CreateAccessToken(string userName, Guid userId);
        void RevokeRefreshToken(string token);
        JsonWebToken RefreshAccessToken(string token);
    }
}