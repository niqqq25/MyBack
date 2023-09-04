using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MyBack.InProcessMessaging;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInProcessMessaging(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(assembly));

        services.AddScoped<InProcessMessenger>();
        services.AddScoped<ISender>(p => p.GetRequiredService<InProcessMessenger>());
        services.AddScoped<IPublisher>(p => p.GetRequiredService<InProcessMessenger>());

        return services;
    }
    
    public static IServiceCollection AddPipelineBehavior(this IServiceCollection services, Type type)
    {
        services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), type);
        return services;
    }
}