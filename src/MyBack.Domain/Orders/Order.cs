using MyBack.Domain.Common.Models;
using MyBack.Domain.Orders.Enums;
using MyBack.Domain.Orders.Events;
using MyBack.Domain.Orders.ValueObjects;

namespace MyBack.Domain.Orders;

public sealed class Order : Entity<OrderId>
{
#pragma warning disable CS8618
    private Order()
#pragma warning disable CS8618
    {
    }
    
    private Order(OrderId id, Address shippingAddress, IReadOnlyCollection<OrderItem> items) : base(id)
    {
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Submitted;
        Items = items;
        ShippingAddress = shippingAddress;
    }

    public DateTime CreatedAt { get; }
    
    public OrderStatus Status { get; }
    
    public Address ShippingAddress { get; }
    
    public IReadOnlyCollection<OrderItem> Items { get; }

    public static Order Place(OrderId id, Address shippingAddress, IReadOnlyCollection<OrderItem> items)
    {
        var order = new Order(id, shippingAddress, items);
        order.AddDomainEvent(new OrderPlacedDomainEvent(order));
        return order;
    }
}