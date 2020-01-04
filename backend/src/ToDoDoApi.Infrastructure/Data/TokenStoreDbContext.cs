using Microsoft.EntityFrameworkCore;
using ToDoDoApi.Core.Entities;

namespace ToDoDoApi.Infrastructure.Data
{
    public class TokenStoreDbContext : DbContext
    {
        public TokenStoreDbContext(DbContextOptions<TokenStoreDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<RefreshToken> Tokens { get; set; }
    }
}