using MyBack.Domain.Common.Models;
using MyBack.Domain.Orders.ValueObjects;
using MyBack.Domain.Products;
using MyBack.Domain.Products.ValueObjects;

namespace MyBack.Domain.Orders;

public sealed class OrderItem : Entity<OrderItemId>
{
#pragma warning disable CS8618
    private OrderItem()
#pragma warning disable CS8618
    {
    }
    
    public OrderItem(OrderItemId id, Product product, int quantity) : base(id)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException($"Quantity must be positive number.", nameof(quantity));
        }
        
        Quantity = quantity;
        Product = product;
        ProductId = product.Id;
        ItemPrice = product.Price;
    }

    public OrderId OrderId { get; }
    
    public ProductId ProductId { get; }
    
    public Product Product { get; }
    
    public decimal ItemPrice { get; }
    
    public int Quantity { get; }
}