using MyBack.Domain.Common.Interfaces;

namespace MyBack.Domain.Common.Models;

public abstract class Entity<TId> : IHasDomainEvents, IEquatable<Entity<TId>> where TId : struct
{
    private List<IDomainEvent>? _domainEvents;
    
    public TId Id { get; protected set; }

    protected Entity()
    {
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }
    
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(domainEvent);
    }
}