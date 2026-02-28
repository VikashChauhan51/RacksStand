namespace RacksStands.Framework.Caching.Options;

public class CacheOptions
{
    public const string SectionName = "Cache";
    public string? RedisConnectionString { get; set; }
    public bool UseHybridCache { get; set; }
}
