using MediatR;
using MyBack.Application.Common.InProcessMessaging.Commands;
using MyBack.Application.Common.InProcessMessaging.Events;
using MyBack.Application.Common.InProcessMessaging.Queries;
using MyBack.Domain.Common.Interfaces;

namespace MyBack.Application.Common.InProcessMessaging;

public class InProcessMessenger : IPublisher, ISender
{
    private readonly IMediator _mediator;

    public InProcessMessenger(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task PublishDomainEvent<TDomainEvent>(
        TDomainEvent domainEvent,
        CancellationToken cancellationToken = default) where TDomainEvent : IDomainEvent
    {
        return _mediator.Publish(domainEvent, cancellationToken);
    }

    public Task PublishIntegrationEvent<TIntegrationEvent>(
        TIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default) where TIntegrationEvent : IIntegrationEvent
    {
        return _mediator.Publish(integrationEvent, cancellationToken);
    }

    public Task<TResponse> SendQuery<TResponse>(
        IQuery<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        return _mediator.Send(request, cancellationToken);
    }

    public Task<TResponse> SendCommand<TResponse>(
        ICommand<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        return _mediator.Send(request, cancellationToken);
    }

    public Task SendCommand<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : ICommand
    {
        return _mediator.Send(request, cancellationToken);
    }
}