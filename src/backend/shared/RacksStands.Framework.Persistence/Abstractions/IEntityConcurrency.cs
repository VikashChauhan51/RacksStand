namespace RacksStands.Framework.Persistence.Abstractions;

public interface IEntityConcurrency: IEntity
{
    string? ConcurrencyStamp { get; set; }
}
