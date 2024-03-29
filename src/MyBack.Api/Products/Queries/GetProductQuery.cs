using Microsoft.EntityFrameworkCore;
using MyBack.Api.Products.Responses;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Domain.Products.ValueObjects;
using MyBack.InProcessMessaging;

namespace MyBack.Api.Products.Queries;

public sealed record GetProductQuery(ProductId Id) : IQuery<GetProductResponse>
{
    public sealed class Handler : IQueryHandler<GetProductQuery, GetProductResponse>
    {
        private readonly IDbContext _dbContext;

        public Handler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            // TODO fix Id.Id
            var product = await _dbContext.Products
                .Where(p => p.Id == request.Id)
                .Select(p => new GetProductResponse(p.Id.Value, p.Name, p.Description, p.Price, p.Stock))
                .SingleAsync(cancellationToken);

            return product;
        }
    }
}