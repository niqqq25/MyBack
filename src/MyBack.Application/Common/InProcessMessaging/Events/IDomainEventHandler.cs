using MediatR;
using MyBack.Domain.Common.Interfaces;

namespace MyBack.Application.Common.InProcessMessaging.Events;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
}