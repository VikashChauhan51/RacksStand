using Microsoft.Extensions.Http.Resilience;
using RacksStands.Framework.Resiliency.Options;
using Polly;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace RacksStands.Framework.Resiliency.Policies;

public static class RetryPolicyFactory
{

    public static HttpRetryStrategyOptions CreateDefaultHttpRetryStrategyOptions(RetryPolicyOptions options) =>
    new()
    {
        MaxRetryAttempts = options.RetryCount,
        Delay = options.BaseDelay,
        BackoffType = DelayBackoffType.Exponential,
        UseJitter = options.EnableJitter
    };

    public static HttpRetryStrategyOptions CreateWithRetryAfterHeader(RetryPolicyOptions options) =>
        new()
        {
            MaxRetryAttempts = options.RetryCount,
            Delay = options.BaseDelay,
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true,
            DelayGenerator = static args =>
            {
                if (args.Outcome.Result.Headers.TryGetValues("Retry-After", out var values) is true)
                {
                    var retryAfterValue = values.FirstOrDefault();
                    if (int.TryParse(retryAfterValue, out var seconds))
                    {
                        return ValueTask.FromResult<TimeSpan?>(TimeSpan.FromSeconds(seconds));
                    }

                    if (DateTimeOffset.TryParse(retryAfterValue,CultureInfo.InvariantCulture, out var retryAfterDate))
                    {
                        var delay = retryAfterDate - DateTimeOffset.UtcNow;
                        if (delay > TimeSpan.Zero)
                        {
                            return ValueTask.FromResult<TimeSpan?>(delay);
                        }
                            
                    }
                }
                return ValueTask.FromResult<TimeSpan?>(null);

            }
        };
}
