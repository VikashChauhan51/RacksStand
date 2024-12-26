using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Events;
public record PocketCapacityReachedEvent
(
    PocketId PocketId
) : DomainEvent;
