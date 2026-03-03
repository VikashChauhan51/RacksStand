using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RacksStands.Framework.Monitoring.Logging.Enrichers;
using RacksStands.Framework.Monitoring.Tracking.Enrichers;
using System;
using System.Collections.Generic;

namespace RacksStands.Framework.Monitoring.Extensions;

public static class MonitoringServiceCollectionExtensions
{
    public static void AddRacksStandsMonitoring(
        this IHostApplicationBuilder builder,
        string serviceName,
        string serviceVersion)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        ArgumentNullException.ThrowIfNull(serviceName, nameof(serviceName));
        ArgumentNullException.ThrowIfNull(serviceVersion, nameof(serviceVersion));
        builder.Services.TryAddSingleton<SecureLogAttributeEnrichmentProcessor>();
        builder.Services.TryAddSingleton<HttpContextEnrichmentProcessor>();

        string environment = builder.Environment.EnvironmentName;
        string applicationName = builder.Environment.ApplicationName;

        var otel = builder.Services.AddOpenTelemetry();
        otel.ConfigureResource(resourceBuilder =>
        {
            resourceBuilder.AddService(serviceName, serviceVersion: serviceVersion)
                .AddAttributes(new[]
                {
                    new KeyValuePair<string, object>("environment", environment),
                    new KeyValuePair<string, object>("application", applicationName)
                });
        });

        otel.WithLogging(loggingBuilder =>
        {
            loggingBuilder.AddProcessor(new LogAttributeEnrichmentProcessor("attribute"));
        });

        otel.Services.Configure<OpenTelemetryLoggerOptions>(logging =>
        {
            logging.ParseStateValues = true;
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        otel.WithTracing(tracingBuilder =>
        {
            tracingBuilder.AddSource(serviceName);
            tracingBuilder.AddProcessor<SecureLogAttributeEnrichmentProcessor>();
            tracingBuilder.AddProcessor<HttpContextEnrichmentProcessor>();
            tracingBuilder.AddAspNetCoreInstrumentation(options =>
            {
                options.RecordException = true;

                options.EnrichWithHttpRequest = (activity, httpRequest) =>
                {
                    activity.SetTag("http.request_content_length", httpRequest.ContentLength);
                    activity.SetTag("http.request_content_type", httpRequest.ContentType);
                };

                options.EnrichWithHttpResponse = (activity, httpResponse) =>
                {
                    activity.SetTag("http.response_content_length", httpResponse.ContentLength);
                    activity.SetTag("http.response_content_type", httpResponse.ContentType);
                };

                options.EnrichWithException = (activity, exception) =>
                {
                    activity.SetTag("exception.type", exception.GetType().FullName);
                    activity.SetTag("exception.message", exception.Message);
                    activity.SetTag("exception.stacktrace", exception.StackTrace);
                };
            })
            .AddHttpClientInstrumentation(options =>
            {
                options.RecordException = true;

                options.EnrichWithHttpWebRequest = (activity, httpRequest) =>
                {
                    activity.SetTag("http.request_content_length", httpRequest.ContentLength);
                    activity.SetTag("http.request_content_type", httpRequest.ContentType);
                };

                options.EnrichWithHttpRequestMessage = (activity, httpResponse) =>
                {
                    if (httpResponse.Content != null)
                    {
                        activity.SetTag("http.response_content_length", httpResponse.Content.Headers.ContentLength);
                        activity.SetTag("http.response_content_type", httpResponse.Content.Headers.ContentType);
                    }
                };
                options.EnrichWithHttpResponseMessage = (activity, httpResponse) =>
                {
                    if (httpResponse.Content != null)
                    {
                        activity.SetTag("http.response_content_length", httpResponse.Content.Headers.ContentLength);
                        activity.SetTag("http.response_content_type", httpResponse.Content.Headers.ContentType);
                    }
                };

                options.EnrichWithException = (activity, exception) =>
                {
                    activity.SetTag("exception.type", exception.GetType().FullName);
                    activity.SetTag("exception.message", exception.Message);
                    activity.SetTag("exception.stacktrace", exception.StackTrace);
                };
            });

            tracingBuilder.AddSqlClientInstrumentation(options =>
            {
                options.RecordException = true;
            });

        });

        otel.WithMetrics(metricsBuilder =>
        {
            metricsBuilder.AddMeter(serviceName);
            metricsBuilder.AddAspNetCoreInstrumentation();
            metricsBuilder.AddHttpClientInstrumentation();
            metricsBuilder.AddRuntimeInstrumentation();
            metricsBuilder.AddSqlClientInstrumentation();
        });

        otel.UseOtlpExporter();
    }
}
