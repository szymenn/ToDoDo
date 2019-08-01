using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoListApi.Models;
namespace ToDoListApi.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IOptions<TokenManagement> _tokenManagement;

        public TokenGenerator(IOptions<TokenManagement> tokenManagement)
        {
            _tokenManagement = tokenManagement;
        }
        
        public string GenerateToken(string userName, Guid userId)
        {
            var claim = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Value.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                _tokenManagement.Value.Issuer,
                _tokenManagement.Value.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.Value.AccessExpiration),
                signingCredentials: credentials);
            
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}