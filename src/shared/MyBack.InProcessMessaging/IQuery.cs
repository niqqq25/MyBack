using MediatR;

namespace MyBack.InProcessMessaging;

public interface IQuery<out T> : IRequest<T>
{
}