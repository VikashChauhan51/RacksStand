using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Events;
public record ProductUpdatedEvent
(
    ProductId ProductId,
    string NewName,
    string? NewDescription = null
) : DomainEvent;
