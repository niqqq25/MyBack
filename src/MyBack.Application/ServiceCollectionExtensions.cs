using Microsoft.Extensions.DependencyInjection;
using MyBack.Application.Common.Behaviors;
using MyBack.InProcessMessaging;

namespace MyBack.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddInProcessMessaging(typeof(ServiceCollectionExtensions).Assembly);
        services.AddPipelineBehavior(typeof(TransactionBehavior<,>));
        //services.AddTransient(typeof(ICommandPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        //services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        // services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(TestBehavior<,>));
        
        return services;
    }
}