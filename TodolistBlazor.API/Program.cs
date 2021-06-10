using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodolistBlazor.API.Data;
using TodolistBlazor.API.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace TodolistBlazor.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.MigrateDbContext<TodolistDbContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<TodolistDbContextSeed>>();
                new TodolistDbContextSeed().SeedAsync(context, logger).Wait();
            });
            host.Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
