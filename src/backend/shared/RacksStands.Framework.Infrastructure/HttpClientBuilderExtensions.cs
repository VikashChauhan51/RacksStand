using Microsoft.Extensions.DependencyInjection;

namespace RacksStands.Framework.Infrastructure;

public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder AddResilientHttpClient(this IHttpClientBuilder builder)
    {
        // Use .NET 8+ built-in resilience instead of Polly
        builder.AddStandardResilienceHandler();

        return builder;
    }
}
