using MyBack.Application.Common.InProcessMessaging.Commands;
using MyBack.Application.Common.InProcessMessaging.Queries;

namespace MyBack.Application.Common.InProcessMessaging;

public interface ISender
{
    Task<TResponse> SendQuery<TResponse>(IQuery<TResponse> request, CancellationToken cancellationToken = default);

    Task<TResponse> SendCommand<TResponse>(ICommand<TResponse> request, CancellationToken cancellationToken = default);

    Task SendCommand<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : ICommand;
}