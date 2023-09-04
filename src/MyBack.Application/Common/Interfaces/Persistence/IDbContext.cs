using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyBack.Domain.Orders;
using MyBack.Domain.Products;

namespace MyBack.Application.Common.Interfaces.Persistence;

public interface IDbContext
{
    DbSet<Product> Products { get; }
    
    DbSet<Order> Orders { get; }

    Task MigrateAsync();
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

    Task CommitTransactionAsync();
}