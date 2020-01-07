using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ToDoListApi.Services
{
    public interface IJwtHandler
    {
        string CreateAccessToken(ICollection<Claim> claims);
    }
}