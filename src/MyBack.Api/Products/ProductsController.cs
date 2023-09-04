using Microsoft.AspNetCore.Mvc;
using MyBack.Api.Common;
using MyBack.Api.Products.Queries;
using MyBack.Api.Products.Requests;
using MyBack.Application;
using MyBack.Application.Products.Commands;
using MyBack.Domain.Products.ValueObjects;
using MyBack.InProcessMessaging;

namespace MyBack.Api.Products;

[Route("api/products")]
public class ProductsController : ApiControllerBase
{
    public ProductsController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request, CancellationToken cancellationToken)
    {
        await _sender.SendCommand(
            new CreateProductCommand(request.Name, request.Description, request.Price, request.Stock),
            cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var products = await _sender.SendQuery(new GetProductsQuery(), cancellationToken);

        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellationToken)
    {
        var product = await _sender.SendQuery(new GetProductQuery(new ProductId(id)), cancellationToken);

        return Ok(product);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(
        Guid id,
        UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        await _sender.SendCommand(
            new UpdateProductCommand(new ProductId(id), request.Name, request.Description, request.Price),
            cancellationToken);

        return Ok();
    }
}