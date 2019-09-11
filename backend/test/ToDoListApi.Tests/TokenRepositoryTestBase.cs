using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using ToDoListApi.Data;
using ToDoListApi.Entities;

namespace ToDoListApi.Tests
{
    public class TokenRepositoryTestBase : IDisposable
    {
        protected TokenStoreDbContext Context { get; }

        protected const string Token = "token";
        protected const string DifferentToken = "different token";
        
        public TokenRepositoryTestBase()
        {
            var options = new DbContextOptionsBuilder<TokenStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            
            Context = new TokenStoreDbContext(options);

            Context.Database.EnsureCreated();
            
            Initialize(Context);
        }
        
        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

        private static void Initialize(TokenStoreDbContext context)
        {
            var tokens = new[]
            {
                new RefreshToken
                {
                    Id = Guid.Parse("4efb956c-a645-4480-9ed9-abc49acb55e0"),
                    Token = Token,
                    Revoked = false,
                    UserName = "username",
                    UserId = Guid.Parse("e2875b2c-faa6-4f40-8175-f2c6e565eb9a")
                },
                new RefreshToken
                {
                    Id = Guid.Parse("d155c5f0-255c-4e33-bd22-647f43f83f30"),
                    Token = DifferentToken,
                    Revoked = true,
                    UserName = "other username",
                    UserId = Guid.Parse("f61c1af3-7755-4fdf-b1b7-c235c5b0193d")
                }
            };
            
            context.Tokens.AddRange(tokens);
            context.SaveChanges();
        }
    }
}