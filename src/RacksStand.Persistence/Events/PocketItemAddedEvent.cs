using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Events;
public record PocketItemAddedEvent
(
    PocketId PocketId,
    ProductId ProductId,
    int Quantity
) : DomainEvent;
