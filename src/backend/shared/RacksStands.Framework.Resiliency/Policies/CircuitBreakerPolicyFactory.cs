using Microsoft.Extensions.Http.Resilience;
using RacksStands.Framework.Resiliency.Options;

namespace RacksStands.Framework.Resiliency.Policies;

public static class CircuitBreakerPolicyFactory
{
    public static HttpCircuitBreakerStrategyOptions Create(CircuitBreakerPolicyOptions options) =>
        new()
        {
            SamplingDuration = options.SamplingDuration,
            FailureRatio = options.FailureRatio,
            MinimumThroughput = options.MinimumThroughput,
            BreakDuration = options.DurationOfBreak
        };
}
