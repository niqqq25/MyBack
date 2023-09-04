using MediatR;

namespace MyBack.InProcessMessaging;

public interface ICommand : IRequest, IBaseCommand
{
}

public interface ICommand<out T> : IRequest<T>, IBaseCommand
{
}

public interface IBaseCommand
{
}