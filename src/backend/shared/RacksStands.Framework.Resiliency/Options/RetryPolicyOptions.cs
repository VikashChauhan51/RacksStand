using System;

namespace RacksStands.Framework.Resiliency.Options;

public class RetryPolicyOptions
{
    public const string SectionName = "RetryPolicy";
    public int RetryCount { get; set; } = 3;
    public TimeSpan BaseDelay { get; set; } = TimeSpan.FromMilliseconds(200);
    public bool EnableJitter { get; set; } = true;

    public TimeSpan TotalTimeout { get; set; } = TimeSpan.FromSeconds(30);
    public TimeSpan AttemptTimeout { get; set; } = TimeSpan.FromSeconds(30);
}
