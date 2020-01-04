using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoDoApi.Core.Entities;

namespace ToDoDoApi.Infrastructure.Data
{
    public class UserStoreDbContext : IdentityDbContext<AppUser>
    {
        public UserStoreDbContext(DbContextOptions options)
            : base(options)
        {
            
        }
    }
}