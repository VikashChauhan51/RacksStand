using Microsoft.Extensions.DependencyInjection;
using Polly;
using RacksStands.Framework.Resiliency.Options;
using RacksStands.Framework.Resiliency.Policies;
using System;

namespace RacksStands.Framework.Resiliency.Extensions;

public static class ResiliencyHttpClientExtensions
{
    public static IHttpClientBuilder AddStandardResilience(
        this IHttpClientBuilder builder,
        Action<RetryPolicyOptions>? configureOptions = null)
    {
        var option = new RetryPolicyOptions();
        configureOptions?.Invoke(option);

        builder.AddResilienceHandler(RetryPolicyOptions.SectionName, pipeline =>
        {
            pipeline.AddTimeout(option.TotalTimeout);
            pipeline.AddRetry(RetryPolicyFactory.CreateDefaultHttpRetryStrategyOptions(option));
            pipeline.AddTimeout(option.AttemptTimeout);
        });
        return builder;
    }

    public static IHttpClientBuilder AddCircuitBreaker(
        this IHttpClientBuilder builder,
        Action<CircuitBreakerPolicyOptions>? configureOptions = null)
    {
        var option = new CircuitBreakerPolicyOptions();
        configureOptions?.Invoke(option);
        builder.AddResilienceHandler(CircuitBreakerPolicyOptions.SectionName, pipeline =>
        {
            pipeline.AddCircuitBreaker(CircuitBreakerPolicyFactory.Create(option));
        });
        return builder;
    }
}
