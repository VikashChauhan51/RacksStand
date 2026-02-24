namespace RacksStand.Api.Domain.Entities.Core;

public abstract class EntityBase<T> : EntityBase, IEntity<T> where T : notnull
{
    public required T Id { get; set; }
}
public abstract class EntityBase : IEntity, IEntityAudit, IEntityConcurrency
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? ConcurrencyStamp { get; set; }
}
