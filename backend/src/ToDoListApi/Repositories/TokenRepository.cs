using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using ToDoListApi.Data;
using ToDoListApi.Entities;
using ToDoListApi.Exceptions;

namespace ToDoListApi.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly TokenStoreDbContext _context;

        public TokenRepository(TokenStoreDbContext context)
        {
            _context = context;
        }
        
        public void SaveRefreshToken(RefreshToken refreshToken)
        {
            if (Exists(refreshToken.Token))
            {
                throw new ResourceAlreadyExistsException();
            }

            _context.Add(refreshToken);
            _context.SaveChanges();
        }

        public void RemoveRefreshToken(string token)
        {
            if (!Exists(token))
            {
                throw new ResourceNotFoundException();
            }

            var refreshToken = _context.Tokens.First(p => p.Token == token);
            _context.Remove(refreshToken);
            _context.SaveChanges();
        }

        public bool CanRefresh(string token)
        {
            if (!Exists(token))
            {
                return false;
            }

            var refreshToken = _context.Tokens.First(p => p.Token == token);
            return !refreshToken.Revoked;
        }

        public void RevokeRefreshToken(string token)
        {
            if (!Exists(token))
            {
                throw new ResourceNotFoundException();
            }

            var refreshToken = _context.Tokens.First(p => p.Token == token);
            refreshToken.Revoked = true;
            _context.SaveChanges();
        }

        public string UpdateRefreshToken(string token, string updateToken)
        {
            if (!Exists(token))
            {
                throw new ResourceNotFoundException();
            }

            var refreshToken = _context.Tokens.First(p => p.Token == token);
            refreshToken.Token = updateToken;
            return updateToken;
        }

        public ICollection<Claim> GetUserClaims(string token)
        {
            if (!Exists(token))
            {
                throw new ResourceNotFoundException();
            }

            var refreshToken = _context.Tokens.First(p => p.Token == token);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, refreshToken.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, refreshToken.UserId.ToString())
            };
            return claims;
        }

        private bool Exists(string token)
        {
            var alreadyExists = _context.Tokens.Any(p => p.Token == token);
            return alreadyExists;
        }

    }
}