using Microsoft.EntityFrameworkCore.Diagnostics;
using MyBack.Domain.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MyBack.Infrastructure.Persistence.Interceptors;

public class PublishDomainEventsOnSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public PublishDomainEventsOnSaveChangesInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await PublishDomainEvents(eventData.Context, cancellationToken);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        PublishDomainEvents(eventData.Context, CancellationToken.None).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    private async Task PublishDomainEvents(DbContext? dbContext, CancellationToken cancellationToken)
    {
        if (dbContext is null)
        {
            return;
        }

        var entitiesWithDomainEvents = dbContext.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(x => x.Entity.DomainEvents is { } de && de.Any())
            .Select(x => x.Entity)
            .ToArray();

        var domainEvents = entitiesWithDomainEvents.SelectMany(x => x.DomainEvents!).ToArray();

        foreach (var domainEntity in entitiesWithDomainEvents)
        {
            domainEntity.ClearDomainEvents();
        }

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }
    }
}