using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBack.Domain.Products;
using MyBack.Domain.Products.ValueObjects;

namespace MyBack.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);
        
        builder
            .Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new ProductId(value));

        builder.Property(x => x.Name).HasMaxLength(Product.MaxNameLength).IsRequired();

        builder.Property(x => x.Description).HasMaxLength(Product.MaxDescriptionLength).IsRequired(false);

        builder.Property(x => x.Stock).IsRequired(false);

        builder.Property(x => x.Price).HasColumnType("decimal(38,0)").IsRequired();
    }
}