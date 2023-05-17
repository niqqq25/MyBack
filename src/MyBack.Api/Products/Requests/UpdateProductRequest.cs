namespace MyBack.Api.Products.Requests;

public record UpdateProductRequest(string? Name, string? Description, decimal? Price);