using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RacksStands.Framework.Monitoring.Options;
using RacksStands.Framework.Monitoring.Extensions;
using RacksStands.Framework.Monitoring.Tracking;
using RacksStands.Framework.Monitoring.Metrics;

namespace RacksStands.Framework.Monitoring.Extensions;

public static class MonitoringServiceCollectionExtensions
{
    public static IServiceCollection AddRacksStandsMonitoring(this IServiceCollection services, IConfiguration configuration)
    {
        // Bind options
        services.Configure<OpenTelemetryOptions>(configuration.GetSection(OpenTelemetryOptions.SectionName));
        services.Configure<MetricsOptions>(configuration.GetSection("Metrics"));
        services.Configure<TrackingOptions>(configuration.GetSection("Tracking"));

        // Add logging, tracing, metrics
        services.AddRacksStandsLogging(configuration);
        services.AddRacksStandsTracking(options => configuration.GetSection("Tracking").Bind(options));
        // Add metrics here if needed
        // services.AddRacksStandsMetrics(...)
        return services;
    }
}
