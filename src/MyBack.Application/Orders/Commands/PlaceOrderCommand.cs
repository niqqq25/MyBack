using Microsoft.EntityFrameworkCore;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Domain.Orders;
using MyBack.Domain.Orders.ValueObjects;
using MyBack.Domain.Products.ValueObjects;
using MyBack.InProcessMessaging;

namespace MyBack.Application.Orders.Commands;

public sealed record PlaceOrderCommand(PlaceOrderCommand.Product[] Products, Address ShippingAddress) : ICommand
{
    public sealed class Handler : ICommandHandler<PlaceOrderCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly ISequentialGuidGenerator _sequentialGuidGenerator;

        public Handler(IDbContext dbContext, ISequentialGuidGenerator sequentialGuidGenerator)
        {
            _dbContext = dbContext;
            _sequentialGuidGenerator = sequentialGuidGenerator;
        }

        public async Task Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var productIds = request.Products.Select(x => x.Id).ToArray();
            var products = await _dbContext.Products.Where(p => productIds.Contains(p.Id))
                .ToArrayAsync(cancellationToken);

            if (productIds.Length != products.Length)
            {
                return;
            }

            var orderItems = request.Products.Select(
                    selectedProduct =>
                    {
                        var product = products.Single(p => p.Id == selectedProduct.Id);

                        // TODO add row versioning
                        if (selectedProduct.Quantity > product.Stock)
                        {
                            throw new Exception(); // TODO fix later
                        }

                        return new OrderItem(
                            new OrderItemId(_sequentialGuidGenerator.Next()),
                            product,
                            selectedProduct.Quantity);
                    })
                .ToArray();
            
            var order = Order.Place(new OrderId(_sequentialGuidGenerator.Next()), request.ShippingAddress, orderItems);

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public record Product(ProductId Id, int Quantity);
}