using Polly;
using Polly.Retry;
using RacksStands.Framework.Resiliency.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace RacksStands.Framework.Resiliency.Policies;

public static class PollingPolicyFactory
{

    public static ResiliencePipeline<HttpResponseMessage> CreatePollingPolicy(PollingPolicyOptions options) =>

        new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                MaxRetryAttempts = options.RetryCount,
                Delay = options.BaseDelay,
                BackoffType = DelayBackoffType.Constant,
                UseJitter = false,
                ShouldHandle = static args =>
                {
                    if (args.Outcome.Exception is not null)
                    {
                        return PredicateResult.False();
                    }

                    return ValueTask.FromResult(args.Outcome.Result.StatusCode == System.Net.HttpStatusCode.Accepted);
                }
            })
            .Build();

}
