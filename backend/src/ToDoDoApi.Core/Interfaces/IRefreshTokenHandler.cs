using System;

namespace ToDoDoApi.Core.Interfaces
{
    public interface IRefreshTokenHandler
    {
        string CreateRefreshToken(string userName, Guid userId);
        string UpdateRefreshToken(string token);
    }
}