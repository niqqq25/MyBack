using Microsoft.EntityFrameworkCore;
using MyBack.Api.Products.Responses;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.InProcessMessaging;

namespace MyBack.Api.Products.Queries;

public sealed class GetProductsQuery : IQuery<GetProductsResponse>
{
    public sealed class Handler : IQueryHandler<GetProductsQuery, GetProductsResponse>
    {
        private readonly IDbContext _dbContext;

        public Handler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            // TODO fix Id.Id
            var products = await _dbContext.Products
                .Select(p => new GetProductsResponse.Product(p.Id.Value, p.Name, p.Description, p.Price))
                .ToArrayAsync(cancellationToken);

            return new GetProductsResponse(products);
        }
    }
}