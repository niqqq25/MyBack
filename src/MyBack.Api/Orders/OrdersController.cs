using Microsoft.AspNetCore.Mvc;
using MyBack.Api.Common;
using MyBack.Api.Orders.Queries;
using MyBack.Api.Orders.Requests;
using MyBack.Application.Orders.Commands;
using MyBack.Domain.Orders.ValueObjects;
using MyBack.Domain.Products.ValueObjects;
using MyBack.InProcessMessaging;

namespace MyBack.Api.Orders;

[Route("api/orders")]
public class OrdersController : ApiControllerBase
{
    public OrdersController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder(PlaceOrderRequest request, CancellationToken cancellationToken)
    {
        var address = request.ShippingAddress;
        await _sender.SendCommand(
            new PlaceOrderCommand(
                request.Products.Select(p => new PlaceOrderCommand.Product(new ProductId(p.Id), p.Quantity)).ToArray(),
                new Address(address.Street, address.City, address.Country, address.ZipCode)),
            cancellationToken);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
    {
        var orders = await _sender.SendQuery(new GetOrdersQuery(), cancellationToken);
        
        return Ok(orders);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrder(Guid id, CancellationToken cancellationToken)
    {
        var order = await _sender.SendQuery(new GetOrderQuery(new OrderId(id)), cancellationToken);
        
        return Ok(order);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateOrder(Guid id)
    {
        return Ok();
    }
}