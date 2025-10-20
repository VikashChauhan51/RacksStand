namespace RacksStand.Api.Core.Caching;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Hybrid;

public class HybridCacheProvider : ICacheProvider
{
    private readonly HybridCache _hybridCache;

    public HybridCacheProvider(HybridCache hybridCache)
    {
        _hybridCache = hybridCache;
    }

    public async Task<byte[]> GetOrSetAsync(
      string key,
      Func<CancellationToken, ValueTask<byte[]>> factory,
      HybridCacheEntryOptions? options = null,
      CancellationToken cancellationToken = default)
    {
        return await _hybridCache.GetOrCreateAsync(
            key,
            factory,
            options,
            cancellationToken: cancellationToken);
    }


    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _hybridCache.RemoveAsync(key, cancellationToken);
    }
}
