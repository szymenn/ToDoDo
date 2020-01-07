using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoListApi.Data;

namespace ToDoListApi.Extensions
{
    public static class MigrationManager
    {
        public static IWebHost MigrateDatabase(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<UserStoreDbContext>() )
                {
                    appContext.Database.Migrate();
                }
                using (var appContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>() )
                {
                    appContext.Database.Migrate();
                }
                using (var appContext = scope.ServiceProvider.GetRequiredService<TokenStoreDbContext>() )
                {
                    appContext.Database.Migrate();
                }
            }
 
            return host;
        } 
    }
}