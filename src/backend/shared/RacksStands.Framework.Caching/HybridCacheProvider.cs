namespace RacksStands.Framework.Caching;

public class HybridCacheProvider : ICacheProvider
{
    private readonly ICacheProvider _memory;
    private readonly ICacheProvider _distributed;
    public HybridCacheProvider(ICacheProvider memory, ICacheProvider distributed)
    {
        _memory = memory;
        _distributed = distributed;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await _memory.GetAsync<T>(key, cancellationToken);
        if (value is not null)
            return value;
        value = await _distributed.GetAsync<T>(key, cancellationToken);
        if (value is not null)
            await _memory.SetAsync(key, value, null, cancellationToken);
        return value;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null, CancellationToken cancellationToken = default)
    {
        await _memory.SetAsync(key, value, absoluteExpiration, cancellationToken);
        await _distributed.SetAsync(key, value, absoluteExpiration, cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _memory.RemoveAsync(key, cancellationToken);
        await _distributed.RemoveAsync(key, cancellationToken);
    }
}
