using System;

namespace RacksStands.Framework.Resiliency.Options;

public class PollingPolicyOptions
{
    public const string SectionName = "PollingPolicy";
    public int RetryCount { get; set; } = 3;
    public TimeSpan BaseDelay { get; set; } = TimeSpan.FromMilliseconds(200);
}
