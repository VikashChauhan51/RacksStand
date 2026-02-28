using Microsoft.Extensions.DependencyInjection;
using RacksStands.Framework.Caching.Abstractions;
using RacksStands.Framework.Caching.Options;
using RacksStands.Framework.Caching.Providers;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RacksStands.Framework.Caching.Extensions;

public static class CacheServiceCollectionExtensions
{
    public static IServiceCollection AddRacksStandsCaching(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<CacheOptions> option)
    {
        var options = new CacheOptions();
        option.Invoke(options);
        services.AddOptions<CacheOptions>()
            .Configure(option);

        services.AddMemoryCache();

        if (!string.IsNullOrEmpty(options.RedisConnectionString))
        {
            services.AddStackExchangeRedisCache(cfg => cfg.Configuration = options.RedisConnectionString);
            services.TryAddSingleton<ICacheProvider, DistributedCacheProvider>();
        }
        else
        {
            services.TryAddSingleton<ICacheProvider, InMemoryCacheProvider>();
        }

        return services;
    }
}
