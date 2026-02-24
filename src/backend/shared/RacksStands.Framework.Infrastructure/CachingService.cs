using Microsoft.Extensions.Caching.Memory;

namespace RacksStands.Framework.Infrastructure;

public class CachingService : ICachingService
{
    private readonly IMemoryCache _cache;

    public CachingService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<string?> GetStringAsync(string key)
    {
        return Task.FromResult(_cache.Get<string>(key));
    }

    public Task SetStringAsync(string key, string value, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = expiration.Value;
        }
        _cache.Set(key, value, options);
        return Task.CompletedTask;
    }

    public Task<byte[]?> GetBytesAsync(string key)
    {
        return Task.FromResult(_cache.Get<byte[]>(key));
    }

    public Task SetBytesAsync(string key, byte[] value, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
        {
            options.AbsoluteExpirationRelativeToNow = expiration.Value;
        }
        _cache.Set(key, value, options);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        _cache.Remove(key);
        return Task.CompletedTask;
    }
}
