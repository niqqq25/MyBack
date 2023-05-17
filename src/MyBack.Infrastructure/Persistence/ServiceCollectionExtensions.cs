using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Infrastructure.Persistence.Interceptors;

namespace MyBack.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ISequentialGuidGenerator, SequentialGuidGenerator>();
        services.AddScoped<PublishDomainEventsOnSaveChangesInterceptor>();

        services.AddDbContext<IDbContext, MyBackDbContext>(
            (provider, builder) =>
            {
                builder.AddInterceptors(provider.GetRequiredService<PublishDomainEventsOnSaveChangesInterceptor>())
                    .UseSqlServer(configuration.GetConnectionString("MyBackDb"));
            });

        return services;
    }
}