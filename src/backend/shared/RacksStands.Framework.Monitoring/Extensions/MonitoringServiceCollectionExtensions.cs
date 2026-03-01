using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RacksStands.Framework.Monitoring.Options;
using RacksStands.Framework.Monitoring.Tracking.Enrichers;
using System;

namespace RacksStands.Framework.Monitoring.Extensions;

public static class MonitoringServiceCollectionExtensions
{
    public static IServiceCollection AddRacksStandsMonitoring(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddOptions<OpenTelemetryOptions>()
            .Bind(configuration.GetSection(OpenTelemetryOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var oelOptions = new OpenTelemetryOptions();
        configuration.Bind(oelOptions);

        services.AddHttpContextAccessor();
        services.TryAddSingleton<AttributeEnrichmentProcessor>();
        services.TryAddSingleton<HttpContextEnrichmentProcessor>();
        // Add OpenTelemetry Tracing
        services.AddOpenTelemetry()
            .ConfigureResource(resourceBuilder =>
            {
                resourceBuilder.AddService(AppDomain.CurrentDomain.FriendlyName);
            })
            .WithTracing(tracingBuilder =>
            {
                //tracingBuilder.AddProcessor(sp => sp.GetRequiredService<AttributeEnrichmentProcessor>());
                //tracingBuilder.AddProcessor(sp => sp.GetRequiredService<HttpContextEnrichmentProcessor>());
                //if (oelOptions.Otlp != null && oelOptions.Otlp.Endpoint != null)
                //{
                //    tracingBuilder.AddOtlpExporter(otlpOptions =>
                //    {
                //        otlpOptions.Endpoint = new Uri(oelOptions.Otlp.Endpoint);
                //        if (!string.IsNullOrEmpty(oelOptions.Otlp.Headers))
                //        {
                //            otlpOptions.Headers = oelOptions.Otlp.Headers;
                //        }
                //    });
                //}
            })
            //.WithMetrics(metricsBuilder =>
            //{
            //    metricsBuilder;
            //})
            .WithLogging(loggingBuilder =>
            {
            });

        return services;
    }
}
