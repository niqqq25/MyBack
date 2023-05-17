namespace MyBack.Api.Orders.Requests;

public record PlaceOrderRequest(PlaceOrderRequest.Product[] Products, PlaceOrderRequest.Address ShippingAddress)
{
    public record Address(string Street, string City, string Country, string ZipCode);

    public record Product(Guid Id, int Quantity);
}