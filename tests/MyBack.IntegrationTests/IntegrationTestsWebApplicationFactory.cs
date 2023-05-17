using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Infrastructure.Persistence;
using Respawn;
using Testcontainers.MsSql;
using Xunit;

namespace MyBack.IntegrationTests;

public class IntegrationTestsWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer;
    private Respawner _respawner = default!;

    public IntegrationTestsWebApplicationFactory()
    {
        _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithCleanUp(true)
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(
            (_, config) =>
            {
                config.AddJsonFile($"{Environment.CurrentDirectory}/appsettings.json");
                config.AddEnvironmentVariables();
            });

        builder.ConfigureTestServices(
            services =>
            {
                services.RemoveAll(typeof(DbContextOptions<MyBackDbContext>));
                
                services.AddDbContext<IDbContext, MyBackDbContext>(
                    (_, options) => { options.UseSqlServer(_dbContainer.GetConnectionString()); });

                // TODO replace our EmailService
            });
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbContainer.GetConnectionString());
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        CreateClient();

        _respawner = await Respawner.CreateAsync(
            _dbContainer.GetConnectionString(),
            new RespawnerOptions
            {
                TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
            });
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}