namespace RacksStand.Api.Domain.Entities.Core;

public interface IEntity
{
}

public interface IEntity<T> : IEntity where T : notnull
{
    T Id { get; set; }
}
