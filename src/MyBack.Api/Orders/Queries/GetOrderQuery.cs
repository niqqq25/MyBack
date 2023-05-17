using MediatR;
using MyBack.Api.Orders.Responses;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Domain.Orders.ValueObjects;

namespace MyBack.Api.Orders.Queries;

public sealed record GetOrderQuery(OrderId Id) : IRequest<GetOrderResponse>
{
    public sealed class Handler : IRequestHandler<GetOrderQuery, GetOrderResponse>
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