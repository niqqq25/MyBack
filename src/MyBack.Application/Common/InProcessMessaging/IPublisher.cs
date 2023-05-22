using MyBack.Application.Common.InProcessMessaging.Events;
using MyBack.Domain.Common.Interfaces;

namespace MyBack.Application.Common.InProcessMessaging;

public interface IPublisher
{
    Task PublishDomainEvent<TDomainEvent>(TDomainEvent domainEvent, CancellationToken cancellationToken = default)
        where TDomainEvent : IDomainEvent;

    Task PublishIntegrationEvent<TIntegrationEvent>(
        TIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default) where TIntegrationEvent : IIntegrationEvent;
}