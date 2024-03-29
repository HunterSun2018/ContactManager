﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ContactManager.Data;
using Microsoft.AspNetCore.Identity;
using ContactManager.Models;

namespace ContactManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();

            // WITH THIS:
           // var host = BuildWebHost(args);
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    //var dbInitializerLogger = services.GetRequiredService<ILogger<DbInitializer>>();

                    //DbInitializer.Initialize(context, userManager, roleManager, dbInitializerLogger).Wait();

                    DbInitialization.SeedDB(context, userManager, roleManager).Wait();

                    SchoolContext context1 = services.GetRequiredService<SchoolContext>();

                    DbInitialization.SeedContosoUniversity(context1);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
           
            host.Run();           
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>();

    }
}
