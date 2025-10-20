namespace RacksStand.Api.Domain.Entities.Core;

public interface IEntityAudit
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
}
