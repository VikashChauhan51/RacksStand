using Microsoft.Extensions.Caching.Distributed;
using RacksStands.Framework.Caching.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RacksStands.Framework.Caching;

public class DistributedCacheProvider : ICacheProvider
{
    private readonly IDistributedCache _cache;
    public DistributedCacheProvider(IDistributedCache cache) => _cache = cache;

    public async Task<byte[]?> GetAsync(string key, CancellationToken cancellationToken = default)
        => await _cache.GetAsync(key, cancellationToken);

    public async Task SetAsync(string key, byte[] value, TimeSpan? absoluteExpiration = null, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions();
        if (absoluteExpiration.HasValue)
            options.SetAbsoluteExpiration(absoluteExpiration.Value);
        await _cache.SetAsync(key, value, options, cancellationToken);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        => _cache.RemoveAsync(key, cancellationToken);

    public async Task<byte[]> GetOrCreateAsync(string key, Func<Task<byte[]>> factory, TimeSpan? absoluteExpiration = null, CancellationToken cancellationToken = default)
    {
        var data = await _cache.GetAsync(key, cancellationToken);
        if (data is not null)
            return data;

        var value = await factory();
        await SetAsync(key, value, absoluteExpiration, cancellationToken);
        return value;
    }
}
