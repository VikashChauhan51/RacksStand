using System;

namespace RacksStands.Framework.Resiliency.Options;

public class CircuitBreakerPolicyOptions
{
    public const string SectionName = "CircuitBreakerPolicy";
    /// <summary>
    /// Failure ratio (0.0-1.0) before breaking.
    /// </summary>
    public double FailureRatio { get; set; } = 0.5;

    /// <summary>
    /// Minimum number of requests in the sampling window before evaluating failure ratio.
    /// </summary>
    public int MinimumThroughput { get; set; } = 5;

    /// <summary>
    /// Duration of the sampling window.
    /// </summary>
    public TimeSpan SamplingDuration { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Duration to break the circuit.
    /// </summary>
    public TimeSpan DurationOfBreak { get; set; } = TimeSpan.FromSeconds(30);
}
