using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace APIAUTH.Data.Context
{
    public class ActivesWContextFactory : IDesignTimeDbContextFactory<ActivesWContext>
    {
        public ActivesWContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString =
                Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ??
                configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ActivesWContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new ActivesWContext(optionsBuilder.Options);
        }
    }
}
