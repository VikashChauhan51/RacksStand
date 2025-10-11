namespace RacksStand.Domain.Core;

public interface IAggregateRoot
{
}

public interface IAggregateRoot<TEvent, TId> : IAggregateRoot
    where TEvent : IDomainEvent where TId : notnull
{
    public TId Id { get; set; }
    IReadOnlyCollection<TEvent> DomainEvents { get; }
    void AddDomainEvent(TEvent domainEvent);
    void RemoveDomainEvent(TEvent domainEvent);
    void ClearDomainEvents();
}
