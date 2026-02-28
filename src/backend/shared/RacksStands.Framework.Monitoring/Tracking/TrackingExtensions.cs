using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using FluentValidation;

namespace RacksStands.Framework.Monitoring.Tracking;

public static class TrackingExtensions
{
    public static IServiceCollection AddRacksStandsTracking(this IServiceCollection services, Action<TrackingOptions> configure)
    {
        var options = new TrackingOptions();
        configure(options);
        var validator = new TrackingOptionsValidator();
        var result = validator.Validate(options);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        if (options.EnableTracing)
        {
            services.AddOpenTelemetryTracing(builder =>
            {
                builder.AddAspNetCoreInstrumentation()
                       .AddHttpClientInstrumentation()
                       .SetResourceBuilder(OpenTelemetry.Resources.ResourceBuilder.CreateDefault().AddService(options.ServiceName ?? "RacksStands"));
            });
        }
        return services;
    }
}
