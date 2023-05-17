using MyBack.Domain.Common.Models;

namespace MyBack.Domain.Orders.ValueObjects;

public sealed class Address : ValueObject
{
    public const int MaxCityLength = 45;
    public const int MaxZipCodeLength = 11;
    public const int MaxStreetLength = 70;
    public const int MaxCountryLength = 56;
    
    public Address(string street, string city, string country, string zipCode)
    {
        Street = street.Trim();
        if (Street.Length > MaxStreetLength)
        {
            throw new ArgumentException($"Street length must not exceed {MaxStreetLength}.", nameof(street));
        }

        City = city.Trim();
        if (City.Length > MaxCityLength)
        {
            throw new ArgumentException($"City length must not exceed {MaxCityLength}.", nameof(city));
        }
        
        Country = country.Trim();
        if (Country.Length > MaxCountryLength)
        {
            throw new ArgumentException($"Country length must not exceed {MaxCountryLength}.", nameof(country));
        }
        
        ZipCode = zipCode.Trim();
        if (ZipCode.Length > MaxZipCodeLength)
        {
            throw new ArgumentException($"ZIP code length must not exceed {MaxZipCodeLength}.", nameof(country));
        }
    }
    
    public string Street { get; }
    
    public string City { get; }
    
    public string Country { get; }
    
    public string ZipCode { get; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return Country;
        yield return ZipCode;
    }
}