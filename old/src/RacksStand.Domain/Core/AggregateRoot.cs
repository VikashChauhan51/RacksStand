namespace RacksStand.Domain.Core;
public abstract class AggregateRoot<TEvent, TId> : IAggregateRoot<TEvent, TId>
    where TEvent : IDomainEvent where TId : notnull
{
    private readonly List<TEvent> _domainEvents = [];
    public TId Id { get; set; } = default!;

    public IReadOnlyCollection<TEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(TEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
    public void RemoveDomainEvent(TEvent domainEvent) => _domainEvents.Remove(domainEvent);
}
