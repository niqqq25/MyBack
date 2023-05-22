using MediatR;
using MyBack.Infrastructure.Persistence;

namespace MyBack.Infrastructure.Behaviours;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly MyBackDbContext _dbContext;

    public TransactionBehaviour(MyBackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}