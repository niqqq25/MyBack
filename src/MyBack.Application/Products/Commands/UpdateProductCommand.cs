using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Domain.Products.ValueObjects;

namespace MyBack.Application.Products.Commands;

public sealed record UpdateProductCommand(ProductId Id, string? Name, string? Description, decimal? Price) : IRequest
{
    public sealed class Handler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IDbContext _dbContext;

        public Handler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.SingleAsync(p => p.Id == request.Id, cancellationToken);

            // TODO update logic

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}