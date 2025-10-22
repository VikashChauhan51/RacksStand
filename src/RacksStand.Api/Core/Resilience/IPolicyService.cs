using Polly;

namespace RacksStand.Api.Core.Resilience;

public interface IPolicyService
{
    IAsyncPolicy<HttpResponseMessage> GetHttpRetryPolicy();
    IAsyncPolicy<HttpResponseMessage> GetHttpCircuitBreakerPolicy();
    IAsyncPolicy GetSqlRetryPolicy();
}
