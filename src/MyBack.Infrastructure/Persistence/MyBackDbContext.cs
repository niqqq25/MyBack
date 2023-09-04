using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyBack.Application.Common.Interfaces.Persistence;
using MyBack.Domain.Common.Interfaces;
using MyBack.Domain.Orders;
using MyBack.Domain.Products;
using MyBack.Infrastructure.Persistence.Interceptors;

namespace MyBack.Infrastructure.Persistence;

public class MyBackDbContext : DbContext, IDbContext
{
    private readonly PublishDomainEventsOnSaveChangesInterceptor _publishDomainEventsInterceptor;
    private IDbContextTransaction _transaction;

    public MyBackDbContext(
        DbContextOptions<MyBackDbContext> options,
        PublishDomainEventsOnSaveChangesInterceptor publishDomainEventsInterceptor) : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();

    public async Task MigrateAsync()
    {
        await Database.MigrateAsync();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }

    public Task CommitTransactionAsync()
    {
        return Database.CommitTransactionAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}