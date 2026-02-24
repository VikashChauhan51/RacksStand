using Microsoft.Extensions.Options;
using Polly;
using RacksStand.Api.Core.Resilience.Configurations;

namespace RacksStand.Api.Core.Resilience;

public sealed class PolicyService : IPolicyService
{
    private readonly PolyOptions _options;
    private readonly ILogger<PolicyService> _logger;

    public PolicyService(IOptions<PolyOptions> options, ILogger<PolicyService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public IAsyncPolicy CreateCustomPolicy(int retryCount, int delayInMilliseconds)
    {
        return PolicyFactory.CreatePolicy(retryCount, delayInMilliseconds, _logger);
    }
    public IAsyncPolicy<HttpResponseMessage> GetHttpRetryPolicy()
    {
        return PolicyFactory.CreateRetryPolicy(_options, _logger);
    }

    public IAsyncPolicy<HttpResponseMessage> GetHttpCircuitBreakerPolicy()
    {
        return PolicyFactory.CreateCircuitBreakerPolicy(_options, _logger);
    }

    public IAsyncPolicy GetSqlRetryPolicy()
    {
        return PolicyFactory.CreateSqlRetryPolicy(_options, _logger);
    }
}

