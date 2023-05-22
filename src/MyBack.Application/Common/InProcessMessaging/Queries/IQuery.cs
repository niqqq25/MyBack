using MediatR;

namespace MyBack.Application.Common.InProcessMessaging.Queries;

public interface IQuery<out T> : IRequest<T>
{
}