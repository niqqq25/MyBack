using MyBack.Api.Orders.Responses;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.InProcessMessaging;

namespace MyBack.Api.Orders.Queries;

public sealed class GetOrdersQuery : IQuery<GetOrdersResponse>
{
    public sealed class Handler : IQueryHandler<GetOrdersQuery, GetOrdersResponse>
    {
        private readonly IDbContext _dbContext;

        public Handler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<GetOrdersResponse> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}