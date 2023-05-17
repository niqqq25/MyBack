using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBack.Infrastructure.Persistence;

namespace MyBack.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }
}