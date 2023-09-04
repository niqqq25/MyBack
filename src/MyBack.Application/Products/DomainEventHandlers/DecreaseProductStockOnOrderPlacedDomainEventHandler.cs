using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Domain.Orders.Events;
using MyBack.InProcessMessaging;

namespace MyBack.Application.Products.DomainEventHandlers;

public class DecreaseProductStockOnOrderPlacedDomainEventHandler : IDomainEventHandler<OrderPlacedDomainEvent>
{
    private readonly IDbContext _dbContext;

    public DecreaseProductStockOnOrderPlacedDomainEventHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(OrderPlacedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new Exception();

        var lwaddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd = 6;

        var heloooo = lwaddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd == 8
            ? "ffffffffffffffffffff"
            : "dddddddddddddddddddd";

        if (true)
        {
        }
        else if ((lwaddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd == 6)
                 && (lwaddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd != 7))
        {
        }
        
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

internal class MyCoolClass
{
    private readonly bool _blalalalalalal = true;
    private readonly bool _kaakkakakakakak = true;
    private readonly bool _lololololololololol = true;
    private readonly bool _kekekekekekekekekekek = true;
    private readonly bool _wajdwadawwdawdawaddaw = true;

    public bool IsSomething()
    {
        return _blalalalalalal
            && _kaakkakakakakak
            && _lololololololololol
            && _kekekekekekekekekekek
            && _wajdwadawwdawdawaddaw;
    }
}