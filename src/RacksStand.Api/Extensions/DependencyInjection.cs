using Microsoft.EntityFrameworkCore;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RacksStand.Api.Endpoints.Features.Categories.Shared;
using RacksStand.Persistence.Contexts;
using RacksStand.Persistence.Repositories;
using Serilog;
using Serilog.Events;

namespace RacksStand.Api.Extensions;

public static class DependencyInjection
{

    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<RacksStandContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<RacksStandContext>());
        builder.Services.AddScoped<IRacksStandContext>(sp => sp.GetRequiredService<RacksStandContext>());
        builder.Services.AddScoped<IdentityContext>(sp => sp.GetRequiredService<RacksStandContext>());
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return builder;
    }
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        return builder;
    }
    public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
    {
        // Configure Serilog
        builder.Host.UseSerilog((context, services, configuration) => configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.OpenTelemetry());

        builder.Services.AddOpenTelemetry()
    .ConfigureResource(resourceBuilder =>
        resourceBuilder.AddService(serviceName: builder.Environment.ApplicationName))
    .WithLogging()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddSource(builder.Environment.ApplicationName))
    .WithMetrics(metricsProviderBuilder =>
        metricsProviderBuilder
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation())
    .UseOtlpExporter();

        return builder;
    }
}
