namespace RacksStand.Api.Core.Extensions;

using Microsoft.Extensions.Caching.Hybrid;
using RacksStand.Api.Core.Caching;
using RacksStand.Api.Core.Serializers;
public static class CachingExtensions
{
    public static void ConfigureCache(this WebApplicationBuilder builder)
    {
        var cacheOptions = new HybridCacheEntryOptions();
        builder.Configuration.GetSection("Caching").Bind(cacheOptions);

        builder.Services.AddHybridCache(options =>
        {
            options.MaximumPayloadBytes = 1024 * 1024;
            options.MaximumKeyLength = 1024;
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = cacheOptions?.Expiration ?? TimeSpan.FromMinutes(5),
                LocalCacheExpiration = cacheOptions?.LocalCacheExpiration ?? TimeSpan.FromMinutes(5)
            };
        });
        builder.Services.AddSingleton<ICacheProvider, HybridCacheProvider>();
        builder.Services.AddSingleton<IEntitySerializer, JsonEntitySerializer>();
    }
}
