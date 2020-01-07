using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ToDoListApi.Services
{
    public interface IRefreshTokenHandler
    {
        string CreateRefreshToken(string userName, Guid userId);
        string UpdateRefreshToken(string token);
    }
}