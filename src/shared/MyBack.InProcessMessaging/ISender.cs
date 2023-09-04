namespace MyBack.InProcessMessaging;

public interface ISender
{
    Task<TResponse> SendQuery<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);

    Task<TResponse> SendCommand<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);

    Task SendCommand<TRequest>(TRequest command, CancellationToken cancellationToken = default)
        where TRequest : ICommand;
}