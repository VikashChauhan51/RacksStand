using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RacksStands.Framework.Resiliency.Options;
using System;

namespace RacksStands.Framework.Resiliency.Extensions;

public static class ResiliencyServiceCollectionExtensions
{
    public static IServiceCollection AddResiliencyPolicies(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<CircuitBreakerPolicyOptions> configCircuitBreakerPolicyOptions = null,
        Action<RetryPolicyOptions> configRetryPolicyOptions = null,
        Action<PollingPolicyOptions> configPollingPolicyOptions = null)
    {
        services.AddOptions<CircuitBreakerPolicyOptions>()
            .Bind(configuration.GetSection(CircuitBreakerPolicyOptions.SectionName))
            .Configure(configCircuitBreakerPolicyOptions)
            .ValidateOnStart();

        services.AddOptions<RetryPolicyOptions>()
            .Bind(configuration.GetSection(RetryPolicyOptions.SectionName))
            .Configure(configRetryPolicyOptions)
            .ValidateOnStart();

        services.AddOptions<PollingPolicyOptions>()
            .Bind(configuration.GetSection(PollingPolicyOptions.SectionName))
            .Configure(configPollingPolicyOptions)
            .ValidateOnStart();

        return services;
    }
}
