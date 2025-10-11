namespace RacksStand.Domain.Core;
public abstract class DomainEvent : IDomainEvent
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
