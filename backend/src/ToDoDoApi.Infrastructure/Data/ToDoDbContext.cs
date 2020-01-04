using Microsoft.EntityFrameworkCore;
using ToDoDoApi.Core.Entities;

namespace ToDoDoApi.Infrastructure.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<ToDo> ToDos { get; set; }
    }
}