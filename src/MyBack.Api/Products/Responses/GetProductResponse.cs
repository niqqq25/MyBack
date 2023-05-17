namespace MyBack.Api.Products.Responses;

public record GetProductResponse(Guid Id, string Name, string? Description, decimal Price, int? Stock);