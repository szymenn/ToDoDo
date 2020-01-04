using System;
using ToDoDoApi.Core.Dtos;

namespace ToDoDoApi.Core.Interfaces
{
    public interface ITokenService
    {
        JsonWebToken CreateAccessToken(string userName, Guid userId);
        void RevokeRefreshToken(string token);
        JsonWebToken RefreshAccessToken(string token);
    }
}