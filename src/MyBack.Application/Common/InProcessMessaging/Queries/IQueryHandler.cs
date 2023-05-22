using MediatR;

namespace MyBack.Application.Common.InProcessMessaging.Queries;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse>
{
}