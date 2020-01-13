using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoListApi.Data;
using ToDoListApi.Helpers;

namespace ToDoListApi.Extensions
{
    public static class MigrationManager
    {
        public static IWebHost MigrateDatabases(this IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<UserStoreDbContext>() )
                {
                    Migrate(appContext);
                }
                using (var appContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>() )
                {
                    Migrate(appContext);
                }
                using (var appContext = scope.ServiceProvider.GetRequiredService<TokenStoreDbContext>() )
                {
                    Migrate(appContext);
                }
            }
 
            return host;
        }

        private static void Migrate(DbContext appContext)
        {
            if (appContext.Database.ProviderName != Constants.InMemoryProvider)
            {
                appContext.Database.Migrate();
            }
        }
    }
}