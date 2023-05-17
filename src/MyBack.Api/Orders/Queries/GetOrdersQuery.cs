using MediatR;
using MyBack.Api.Orders.Responses;
using MyBack.Application.Common.Interfaces.Persistence;

namespace MyBack.Api.Orders.Queries;

public sealed class GetOrdersQuery : IRequest<GetOrdersResponse>
{
    public sealed class Handler : IRequestHandler<GetOrdersQuery, GetOrdersResponse>
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