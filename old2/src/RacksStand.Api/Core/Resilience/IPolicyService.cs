using Polly;

namespace RacksStand.Api.Core.Resilience;

public interface IPolicyService
{
    IAsyncPolicy CreateCustomPolicy(int retryCount, int delayInMilliseconds);
    IAsyncPolicy<HttpResponseMessage> GetHttpRetryPolicy();
    IAsyncPolicy<HttpResponseMessage> GetHttpCircuitBreakerPolicy();
    IAsyncPolicy GetSqlRetryPolicy();
}
