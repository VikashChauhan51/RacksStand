namespace RacksStands.Framework.Infrastructure;

public interface ICachingService
{
    Task<string?> GetStringAsync(string key);
    Task SetStringAsync(string key, string value, TimeSpan? expiration = null);
    Task<byte[]?> GetBytesAsync(string key);
    Task SetBytesAsync(string key, byte[] value, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
}
