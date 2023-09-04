using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Application.Orders.Commands;
using MyBack.Domain.Orders.Enums;
using MyBack.Domain.Orders.ValueObjects;
using MyBack.Domain.Products;
using MyBack.Domain.Products.ValueObjects;
using Xunit;

namespace MyBack.IntegrationTests.Application.Orders.Commands;

public sealed class PlaceOrderCommandTests : ApplicationTestBase
{
    public PlaceOrderCommandTests(IntegrationTestsWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ProductDoesNotExist_ReturnsValidationError()
    {
        // Act
        await SendCommandAsync(
            new PlaceOrderCommand(
                new[] { new PlaceOrderCommand.Product(new ProductId(Guid.NewGuid()), 1) },
                new Address("Street", "City", "Country", "ZipCode")));

        // Assert
        using (var scope = Factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IDbContext>();
            // TODO check for validation error
            var orders = await db.Orders.ToArrayAsync();
            orders.Should().BeEmpty();
        }
    }

    [Fact]
    public async Task ValidOrder_PlacesOrder()
    {
        // Arrange
        var shippingAddress = new Address("Street", "City", "Country", "ZipCode");
        var productId = new ProductId(Guid.NewGuid());
        
        using (var scope = Factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var product = Product.Create(productId, "Apple", null, 10, null);
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
        }

        // Act
        await SendCommandAsync(
            new PlaceOrderCommand(
                new[] { new PlaceOrderCommand.Product(productId, 1) },
                shippingAddress));

        // Assert
        using (var scope = Factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var orders = await db.Orders.Include(o => o.Items).ToArrayAsync();
            orders.Should().HaveCount(1);
            var order = orders.Single();
            order.Status.Should().Be(OrderStatus.Submitted);
            order.ShippingAddress.Should().Be(shippingAddress);

            order.Items.Should().HaveCount(1);
            var orderItem = order.Items.Single();
            orderItem.Should()
                .BeEquivalentTo(
                    new
                    {
                        ProductId = productId,
                        Quantity = 1,
                        ItemPrice = 10
                    });

            // TODO check or email integration event is called
        }
    }
    
    [Fact]
    public async Task ProductHasLimitedStock_ProductStockDecreases()
    {
        // Arrange
        var shippingAddress = new Address("Street", "City", "Country", "ZipCode");
        var productId = new ProductId(Guid.NewGuid());

        using (var scope = Factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var product = Product.Create(productId, "Apple", null, 10, 5);
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
        }

        // Act
        await SendCommandAsync(
            new PlaceOrderCommand(
                new[] { new PlaceOrderCommand.Product(productId, 1) },
                shippingAddress));

        // Assert
        using (var scope = Factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var product = await db.Products.SingleAsync();
            product.Stock.Should().Be(4);
        }
    }
}