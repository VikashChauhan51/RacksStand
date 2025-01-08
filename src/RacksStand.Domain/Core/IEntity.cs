namespace RacksStand.Domain.Core;
public interface IEntity<T> : IEntity where T : notnull
{
    public T Id { get; set; }
}

public interface IEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public string ConcurrencyStamp { get; set; }
    public bool IsActive { get; set; }
}

