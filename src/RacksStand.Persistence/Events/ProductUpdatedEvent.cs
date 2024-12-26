using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Events;
public record ProductUpdatedEvent
(
    ProductId ProductId,
    string NewName,
    string? NewDescription = null
) : DomainEvent;
