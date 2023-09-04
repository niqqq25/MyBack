using MyBack.Api.Orders.Responses;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Domain.Orders.ValueObjects;
using MyBack.InProcessMessaging;

namespace MyBack.Api.Orders.Queries;

public sealed record GetOrderQuery(OrderId Id) : IQuery<GetOrderResponse>
{
    public sealed class Handler : IQueryHandler<GetOrderQuery, GetOrderResponse>
    {
        private readonly IDbContext _dbContext;

        public Handler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<GetOrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}