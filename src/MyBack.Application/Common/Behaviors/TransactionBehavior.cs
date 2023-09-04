using Microsoft.Extensions.Logging;
using MyBack.Application.Common.Extensions;
using MyBack.Application.Common.Interfaces.Persistence; // TODO maybe abstract this one
using MyBack.InProcessMessaging;

namespace MyBack.Application.Common.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : PipelineBehaviorBase<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly IDbContext _dbContext;
    
    public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    protected override async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();
        
        try
        {
            await using (_ = await _dbContext.BeginTransactionAsync(cancellationToken))
            {
                _logger.LogInformation(
                    "----- Begin transaction for {CommandName} ({@Command})",
                    typeName,
                    request);
        
                response = await next();
        
                _logger.LogInformation(
                    "----- Commit transaction for {CommandName}",
                    typeName);
        
                await _dbContext.CommitTransactionAsync();
            }
        
            // await _integrationEventService.PublishEventsAsync();
        
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Handling transaction for {CommandName} ({@Command})", typeName, request);
            throw;
        }
    }
}