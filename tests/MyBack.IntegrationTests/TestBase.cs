using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MyBack.InProcessMessaging;
using Xunit;

namespace MyBack.IntegrationTests;

public abstract class TestBase : IAsyncLifetime
{
    private readonly Func<Task> _resetDatabase;
    
    protected TestBase(IntegrationTestsWebApplicationFactory factory)
    {
        _resetDatabase = factory.ResetDatabaseAsync;
        Factory = factory;
    }
    
    protected WebApplicationFactory<Program> Factory { get; }
    
    protected async Task<TResponse> SendCommandAsync<TResponse>(ICommand<TResponse> request)
    {
        using var scope = Factory.Services.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.SendCommand(request);
    }

    protected async Task SendCommandAsync(ICommand request)
    {
        using var scope = Factory.Services.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        await mediator.SendCommand(request);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();
}