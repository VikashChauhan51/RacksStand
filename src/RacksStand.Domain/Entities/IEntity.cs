namespace RacksStand.Domain.Entities;
public interface IEntity<T> : IEntity where T : notnull
{
    public T Id { get; set; }
}

public interface IEntity
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; } 
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
