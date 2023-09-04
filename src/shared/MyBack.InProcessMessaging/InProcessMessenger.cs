namespace MyBack.InProcessMessaging;

internal class InProcessMessenger : ISender, IPublisher
{
    private readonly MediatR.IMediator _mediator;

    public InProcessMessenger(MediatR.IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TResponse> SendQuery<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default)
    {
        return _mediator.Send(query, cancellationToken);
    }

    public Task<TResponse> SendCommand<TResponse>(
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }

    public Task SendCommand<TRequest>(TRequest command, CancellationToken cancellationToken = default)
        where TRequest : ICommand
    {
        return _mediator.Send(command, cancellationToken);
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
}