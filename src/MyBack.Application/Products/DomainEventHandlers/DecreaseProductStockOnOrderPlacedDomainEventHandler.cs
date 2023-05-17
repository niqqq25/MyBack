using MediatR;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Domain.Orders.Events;

namespace MyBack.Application.Products.DomainEventHandlers;

public class DecreaseProductStockOnOrderPlacedDomainEventHandler : INotificationHandler<OrderPlacedDomainEvent>
{
    private readonly IDbContext _dbContext;

    public DecreaseProductStockOnOrderPlacedDomainEventHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(OrderPlacedDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var orderItem in notification.Order.Items)
        {
            if (orderItem.Product.Stock.HasValue)
            {
                orderItem.Product.AddStock(-orderItem.Quantity);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}