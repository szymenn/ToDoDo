using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using ToDoListApi.Extensions;

namespace ToDoListApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().MigrateDatabase().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) => { logging.AddEventSourceLogger(); })
                .UseStartup<Startup>()
                .UseUrls("http://*:"+Environment.GetEnvironmentVariable("PORT"));
    }
}