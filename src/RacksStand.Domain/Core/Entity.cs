namespace RacksStand.Domain.Core;
public abstract class Entity<T> : IEntity<T> where T : notnull
{
    public T Id { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        if (ReferenceEquals(this, obj)) return true;

        if (obj is Entity<T> entity)
        {
            return Id.Equals(entity.Id);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
