using MyBack.Domain.Common.Exceptions;
using MyBack.Domain.Common.Models;
using MyBack.Domain.Products.ValueObjects;

namespace MyBack.Domain.Products;

public sealed class Product : Entity<ProductId>
{
    public const int MaxNameLength = 40;
    public const int MaxDescriptionLength = 200;
    
#pragma warning disable CS8618
    private Product()
#pragma warning disable CS8618
    {
    }

    private Product(ProductId id, string name, string? description, decimal price, int? stock) : base(id)
    {
        Name = name.Trim();
        if (Name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Name must not exceed {MaxNameLength}.", nameof(name));
        }

        Description = description?.Trim();
        if (Description is not null && Description.Length > MaxDescriptionLength)
        {
            throw new ArgumentException($"Description must not exceed {MaxDescriptionLength}.", nameof(description));
        }

        if (price <= 0)
        {
            throw new ArgumentException($"Price must be positive number.", nameof(price));
        }

        Price = price;

        if (stock <= 0)
        {
            throw new ArgumentException($"Stock must be positive number.", nameof(stock));
        }
        
        Stock = stock;
    }

    public string Name { get; private set; }

    public string? Description { get; }

    public decimal Price { get; }

    public int? Stock { get; private set; }

    public void AddStock(int stockToAdd)
    {
        if (!Stock.HasValue)
        {
            throw new DomainException("Stock is not set.");
        }

        Stock += stockToAdd; // TODO validate or its not negative
    }

    public static Product Create(ProductId id, string name, string? description, decimal price, int? stock)
    {
        return new Product(id, name, description, price, stock);
    }
}