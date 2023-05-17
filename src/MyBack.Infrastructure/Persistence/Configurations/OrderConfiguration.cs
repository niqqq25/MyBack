using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBack.Domain.Orders;
using MyBack.Domain.Orders.ValueObjects;

namespace MyBack.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);
        
        builder
            .Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new OrderId(value));

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.Status).IsRequired();

        builder.OwnsOne(x => x.ShippingAddress, address =>
        {
            address.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(Address.MaxCityLength)
                .HasColumnName(nameof(Address.City));
            
            address.Property(x => x.ZipCode)
                .IsRequired()
                .HasMaxLength(Address.MaxZipCodeLength)
                .HasColumnName(nameof(Address.ZipCode));
            
            address.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(Address.MaxStreetLength)
                .HasColumnName("Address");
            
            address.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(Address.MaxCountryLength)
                .HasColumnName(nameof(Address.Country));
        });

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}