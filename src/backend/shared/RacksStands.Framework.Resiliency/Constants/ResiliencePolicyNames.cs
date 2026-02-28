using System;
using System.Collections.Generic;
using System.Text;

namespace RacksStands.Framework.Resiliency.Constants;

public static class ResiliencePolicyNames
{
    public const string RetryPolicy = "RetryPolicy";
    public const string CircuitBreakerPolicy = "CircuitBreakerPolicy";
    public const string TimeoutPolicy = "TimeoutPolicy";
    public const string FallbackPolicy = "FallbackPolicy";
}
