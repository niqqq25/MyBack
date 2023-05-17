namespace MyBack.Api.Products.Responses;

public record GetProductsResponse(GetProductsResponse.Product[] Products)
{
    public record Product(Guid Id, string Name, string? Description, decimal Price);
}