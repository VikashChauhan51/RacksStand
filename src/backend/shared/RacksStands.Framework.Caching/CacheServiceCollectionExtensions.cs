using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RacksStands.Framework.Caching;

public static class CacheServiceCollectionExtensions
{
    public static IServiceCollection AddRacksStandsCaching(this IServiceCollection services, Action<CacheOptions> configure)
    {
        var options = new CacheOptions();
        configure(options);
        var validator = new CacheOptionsValidator();
        var result = validator.Validate(options);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        services.AddMemoryCache();
        if (!string.IsNullOrEmpty(options.RedisConnectionString))
        {
            services.AddStackExchangeRedisCache(cfg => cfg.Configuration = options.RedisConnectionString);
        }

        // Register providers as singletons, avoid duplicate registration
        if (!services.Any(s => s.ServiceType == typeof(InMemoryCacheProvider)))
            services.AddSingleton<InMemoryCacheProvider>();
        if (!services.Any(s => s.ServiceType == typeof(DistributedCacheProvider)))
            services.AddSingleton<DistributedCacheProvider>();
        if (!services.Any(s => s.ServiceType == typeof(HybridCacheProvider)))
            services.AddSingleton<HybridCacheProvider>();

        // Register ICacheProvider as Hybrid or fallback
        if (options.UseHybridCache)
            services.AddSingleton<ICacheProvider, HybridCacheProvider>();
        else if (!string.IsNullOrEmpty(options.RedisConnectionString))
            services.AddSingleton<ICacheProvider, DistributedCacheProvider>();
        else
            services.AddSingleton<ICacheProvider, InMemoryCacheProvider>();

        return services;
    }
}
