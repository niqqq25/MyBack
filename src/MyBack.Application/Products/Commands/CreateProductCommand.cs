using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Domain.Products;
using MyBack.Domain.Products.ValueObjects;
using MyBack.InProcessMessaging;

namespace MyBack.Application.Products.Commands;

public sealed record CreateProductCommand(string Name, string? Description, decimal Price, int? Stock) : ICommand
{
    public sealed class Handler : ICommandHandler<CreateProductCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly ISequentialGuidGenerator _sequentialGuidGenerator;

        public Handler(IDbContext dbContext, ISequentialGuidGenerator sequentialGuidGenerator)
        {
            _dbContext = dbContext;
            _sequentialGuidGenerator = sequentialGuidGenerator;
        }

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(
                new ProductId(_sequentialGuidGenerator.Next()),
                request.Name,
                request.Description,
                request.Price,
                request.Stock);

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}