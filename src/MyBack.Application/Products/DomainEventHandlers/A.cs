using MediatR;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Domain.Orders.Events;

namespace MyBack.Application.Products.DomainEventHandlers;

public class A : INotificationHandler<OrderPlacedDomainEvent>
{
    private readonly IDbContext _dbContext;

    public A(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(OrderPlacedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}