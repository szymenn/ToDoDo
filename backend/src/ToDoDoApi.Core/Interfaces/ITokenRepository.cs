using System.Collections.Generic;
using System.Security.Claims;
using ToDoDoApi.Core.Entities;

namespace ToDoDoApi.Core.Interfaces
{
    public interface ITokenRepository
    {
        void SaveRefreshToken(RefreshToken refreshToken);
        void RemoveRefreshToken(string token);
        bool CanRefresh(string token);
        void RevokeRefreshToken(string token);
        string UpdateRefreshToken(string token, string updateToken);
        ICollection<Claim> GetUserClaims(string token);
    }
}