namespace MyBack.Api.Orders.Responses;

public record GetOrdersResponse(GetOrdersResponse.Order[] Orders)
{
    public record Order(Guid Id, DateTime CreatedAt, decimal TotalPrice);
}