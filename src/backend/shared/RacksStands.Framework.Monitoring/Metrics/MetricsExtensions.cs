using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using FluentValidation;

namespace RacksStands.Framework.Monitoring.Metrics;

public static class MetricsExtensions
{
    public static IServiceCollection AddRacksStandsMetrics(this IServiceCollection services, Action<MetricsOptions> configure)
    {
        var options = new MetricsOptions();
        configure(options);
        var validator = new MetricsOptionsValidator();
        var result = validator.Validate(options);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        if (options.EnableMetrics)
        {
            services.AddOpenTelemetryMetrics(builder =>
            {
                builder.AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation();
            });
        }
        return services;
    }
}
