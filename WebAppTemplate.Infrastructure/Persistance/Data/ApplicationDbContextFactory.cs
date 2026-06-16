using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using WebAppTemplate.Domain.Shared.Constants;

namespace WebAppTemplate.Infrastructure.Persistance.Data
{
    public class ApplicationDbContextFactory
    : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var currentDir = Directory.GetCurrentDirectory();

            
            var apiDirCandidate = Path.Combine(currentDir, "WebAppTemplate.API");
            var basePath =
                File.Exists(Path.Combine(currentDir, "appsettings.json")) ? currentDir :
                Directory.Exists(apiDirCandidate) && File.Exists(Path.Combine(apiDirCandidate, "appsettings.json")) ? apiDirCandidate :
                currentDir;

            var environment =
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? "Development";

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder =
                new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(
                configuration.GetConnectionString(ConnectionNames.Local),
                sqlOptions => sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null));

            return new ApplicationDbContext(
                optionsBuilder.Options);
        }
    }
}
