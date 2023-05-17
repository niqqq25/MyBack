using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
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
    
    protected async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = Factory.Services.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(request);
    }

    protected async Task SendAsync(IBaseRequest request)
    {
        using var scope = Factory.Services.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        await mediator.Send(request);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();
}