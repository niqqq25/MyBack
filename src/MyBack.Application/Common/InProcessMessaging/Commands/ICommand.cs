using MediatR;

namespace MyBack.Application.Common.InProcessMessaging.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<out T> : IRequest<T>
{
}