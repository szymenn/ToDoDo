using System.Collections.Generic;
using System.Security.Claims;

namespace ToDoDoApi.Core.Interfaces
{
    public interface IJwtHandler
    {
        string CreateAccessToken(ICollection<Claim> claims);
    }
}