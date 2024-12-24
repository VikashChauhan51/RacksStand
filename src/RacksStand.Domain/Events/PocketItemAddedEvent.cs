using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Events;
public record PocketItemAddedEvent
(
    PocketId PocketId,
    ProductId ProductId,
    int Quantity
) : DomainEvent;
