using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBack.Domain.Orders;
using MyBack.Domain.Orders.ValueObjects;

namespace MyBack.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(x => x.Id);
        
        builder
            .Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new OrderItemId(value));

        builder.Property(x => x.Quantity).IsRequired();

        builder.Property(x => x.ItemPrice).HasColumnType("decimal(38,0)").IsRequired();

        builder.HasOne(x => x.Product)
            .WithOne()
            .HasForeignKey<OrderItem>(x => x.ProductId)
            .IsRequired();

        builder.HasOne<Order>()
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.OrderId)
            .IsRequired();
    }
}