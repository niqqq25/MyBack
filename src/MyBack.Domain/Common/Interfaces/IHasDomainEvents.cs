namespace MyBack.Domain.Common.Interfaces;

public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent>? DomainEvents { get; }

    void ClearDomainEvents();
}