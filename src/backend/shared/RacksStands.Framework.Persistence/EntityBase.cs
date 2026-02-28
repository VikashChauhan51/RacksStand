namespace RacksStands.Framework.Persistence;

public abstract class EntityBase<TKey>
{
    public TKey Id { get; set; } = default!;
}
