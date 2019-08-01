using System;

namespace ToDoListApi.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(string userName, Guid userId);
    }
}