using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Entities;

namespace ToDoListApi.Data
{
    public class UserStoreDbContext : IdentityDbContext<AppUser>
    {
        public UserStoreDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}