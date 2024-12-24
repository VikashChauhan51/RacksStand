using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Events;
public record PocketCapacityReachedEvent
(
    PocketId PocketId
) : DomainEvent;
