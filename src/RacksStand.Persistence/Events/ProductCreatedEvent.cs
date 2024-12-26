using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Events;
public record ProductCreatedEvent
(
    ProductId ProductId,
    string ProductName,
    string ProductCode
) : DomainEvent;
