using MyBack.Domain.Common.Interfaces;

namespace MyBack.Domain.Orders.Events;

public record OrderPlacedDomainEvent(Order Order) : IDomainEvent;