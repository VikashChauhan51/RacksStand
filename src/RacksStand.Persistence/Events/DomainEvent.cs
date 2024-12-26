namespace RacksStand.Persistence.Events;
public abstract record DomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();  
    public DateTime OccurredOn { get; } = DateTime.UtcNow;  
}
