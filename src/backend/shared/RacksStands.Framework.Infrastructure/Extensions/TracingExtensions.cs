using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenTelemetry.Resources;
using RacksStands.Framework.Infrastructure.Tracing;
using RacksStands.Framework.Infrastructure.Tracing.Enrichers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacksStands.Framework.Infrastructure.Extensions;

public static class TracingExtensions
{
    public static IServiceCollection AddDistributedTracing(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        services.AddOptions<OpenTelemetryOptions>()
            .Bind(configuration.GetSection(OpenTelemetryOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var oelOptions = configuration.GetSection(OpenTelemetryOptions.SectionName).BindAndValidateEagerly();
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
                tracingBuilder.AddProcessor(sp => sp.GetRequiredService<AttributeEnrichmentProcessor>());
                tracingBuilder.AddProcessor(sp => sp.GetRequiredService<HttpContextEnrichmentProcessor>());
                if (oelOptions.Otlp != null && oelOptions.Otlp.Endpoint != null)
                {
                    tracingBuilder.AddOtlpExporter(otlpOptions =>
                    {
                        otlpOptions.Endpoint = new Uri(oelOptions.Otlp.Endpoint);
                        if (!string.IsNullOrEmpty(oelOptions.Otlp.Headers))
                        {
                            otlpOptions.Headers = oelOptions.Otlp.Headers;
                        }
                    });
                }
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

    public static OpenTelemetryOptions BindAndValidateEagerly(this IConfiguration configuration)
    {
        var options = new OpenTelemetryOptions();
        configuration.Bind(options);
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(options);
        if (!Validator.TryValidateObject(options, validationContext, validationResults, true))
        {
            var errors = string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage));
            throw new InvalidOperationException($"OpenTelemetry configuration is invalid: {Environment.NewLine}{errors}");
        }
        return options;
    }
}
