namespace RacksStand.Api.Domain.Entities.Core;

public interface IEntityConcurrency
{
    string? ConcurrencyStamp { get; set; }
}
