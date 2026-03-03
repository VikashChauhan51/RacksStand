namespace RacksStands.Framework.Persistence.Abstractions;

public interface IEntityAudit: IEntity
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
}

