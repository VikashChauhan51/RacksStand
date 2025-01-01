namespace RacksStand.Domain.Core;
public abstract class DomainEvent : IDomainEvent
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
