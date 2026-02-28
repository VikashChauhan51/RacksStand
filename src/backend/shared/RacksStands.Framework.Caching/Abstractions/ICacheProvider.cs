using System;
using System.Threading;
using System.Threading.Tasks;

namespace RacksStands.Framework.Caching.Abstractions;

public interface ICacheProvider
{
    Task<byte[]?> GetAsync(string key, CancellationToken cancellationToken = default);
    Task SetAsync(string key, byte[] value, TimeSpan? absoluteExpiration = null, CancellationToken cancellationToken = default);
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task<byte[]> GetOrCreateAsync(string key, Func<Task<byte[]>> factory, TimeSpan? absoluteExpiration = null, CancellationToken cancellationToken = default); 
}
