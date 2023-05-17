namespace MyBack.Api.Products.Requests;

public record CreateProductRequest(string Name, string? Description, decimal Price, int? Stock);