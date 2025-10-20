namespace RacksStand.Api.Domain.Entities;

public class CacheStore
{
    public string Key { get; set; } = default!;

    public byte[] Value { get; set; } = default!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? ExpiresAt { get; set; }
}

