using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Events;
public record ProductCreatedEvent
(
    ProductId ProductId,
    string ProductName,
    string ProductCode
) : DomainEvent;
