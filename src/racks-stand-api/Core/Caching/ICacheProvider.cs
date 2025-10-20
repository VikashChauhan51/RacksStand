using Microsoft.Extensions.Caching.Hybrid;

namespace RacksStand.Api.Core.Caching;

public interface ICacheProvider
{
    /// <summary>
    /// Tries to get a cached item. If not found, executes the factory function and stores the result in cache.
    /// </summary>
    Task<byte[]> GetOrSetAsync(
        string key,
        Func<CancellationToken, ValueTask<byte[]>> factory,
        HybridCacheEntryOptions? options = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a cache entry by its key.
    /// </summary>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}
