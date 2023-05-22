using MediatR;

namespace MyBack.Application.Common.InProcessMessaging.Events;

public interface IIntegrationEventHandler<in TIntegrationEvent> : INotificationHandler<TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
}