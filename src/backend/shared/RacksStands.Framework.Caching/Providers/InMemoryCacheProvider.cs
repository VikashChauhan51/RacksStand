using Microsoft.Extensions.Caching.Memory;
using RacksStands.Framework.Caching.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RacksStands.Framework.Caching.Providers;

public class InMemoryCacheProvider : ICacheProvider
{
    private readonly IMemoryCache _cache;
    public InMemoryCacheProvider(IMemoryCache cache) => _cache = cache;

    public Task<byte[]?> GetAsync(string key, CancellationToken cancellationToken = default)
        => Task.FromResult(_cache.TryGetValue(key, out var value) ? (byte[])value! : null);

    public Task SetAsync(string key, byte[] value, TimeSpan? absoluteExpiration = null, CancellationToken cancellationToken = default)
    {
        var options = new MemoryCacheEntryOptions();
        if (absoluteExpiration.HasValue)
            options.SetAbsoluteExpiration(absoluteExpiration.Value);
        _cache.Set(key, value, options);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        _cache.Remove(key);
        return Task.CompletedTask;
    }

    public async Task<byte[]> GetOrCreateAsync(string key, Func<Task<byte[]>> factory, TimeSpan? absoluteExpiration = null, CancellationToken cancellationToken = default)
    {
        if (_cache.TryGetValue(key, out var value))
            return (byte[])value!;

        var created = await factory();
        await SetAsync(key, created, absoluteExpiration, cancellationToken);
        return created;
    }
}
