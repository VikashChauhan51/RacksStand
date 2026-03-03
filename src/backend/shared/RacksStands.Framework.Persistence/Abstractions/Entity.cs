namespace RacksStands.Framework.Persistence.Abstractions;

public abstract class Entity : IEntity
{

}

public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    public required TKey Id { get; set; }
}


public abstract class AuditableEntity : Entity, IEntityAudit
{
    public DateTimeOffset? DeletedAt { get; set; } = DateTimeOffset.UtcNow;
    DateTimeOffset IEntityAudit.CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    DateTimeOffset? IEntityAudit.UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}


public abstract class ConcurrencyEntity : Entity, IEntityConcurrency
{
    public string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}
