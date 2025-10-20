using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using RacksStand.Api.Core.Logging;

namespace RacksStand.Api.Core.Extensions;

public static class OpenTelemetryExtensions
{
    public static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
    {
        // Setup logging to be exported via OpenTelemetry
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
            logging.AddProcessor(new AttributesPrefixProcessor());
        });

        var otel = builder.Services.AddOpenTelemetry();

        // Add Metrics for ASP.NET Core and our custom metrics and export via OTLP
        otel.WithMetrics(metrics =>
        {
            // Metrics provider from OpenTelemetry
            metrics.AddAspNetCoreInstrumentation();
            metrics.AddSqlClientInstrumentation();
            metrics.AddHttpClientInstrumentation();
            metrics.AddRuntimeInstrumentation();
            // Metrics provides by ASP.NET Core in .NET 8
            metrics.AddMeter("Microsoft.AspNetCore.Hosting");
            metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
        });

        // Add Tracing for ASP.NET Core and our custom ActivitySource and export via OTLP
        otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();
            tracing.AddSqlClientInstrumentation();
            tracing.AddEntityFrameworkCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();
            tracing.AddProcessor<HttpContextEnrichmentProcessor>();
        });

        otel.UseOtlpExporter();
    }
}
