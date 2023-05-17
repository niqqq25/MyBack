using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyBack.Infrastructure.Persistence.Interceptors;

namespace MyBack.Infrastructure.Persistence;

public class MyBackDbContextFactory : IDesignTimeDbContextFactory<MyBackDbContext>
{
    public MyBackDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory()
            + string.Format("{0}..{0}MyBack.Api", Path.DirectorySeparatorChar);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var builder = new DbContextOptionsBuilder<MyBackDbContext>();
        var connectionString = configuration.GetConnectionString("MyBackDb");

        builder.UseSqlServer(connectionString);

        return new MyBackDbContext(builder.Options, new PublishDomainEventsOnSaveChangesInterceptor(null!));
    }
}