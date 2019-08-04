using System;

namespace ToDoListApi.Services
{
    public interface ITokenService
    {
        string GenerateToken(string userName, Guid userId);
    }
}