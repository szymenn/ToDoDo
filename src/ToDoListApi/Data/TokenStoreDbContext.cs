using Microsoft.EntityFrameworkCore;
using ToDoListApi.Entities;

namespace ToDoListApi.Data
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