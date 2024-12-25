using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using Serilog;
using Serilog.Events;
using OpenTelemetry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RacksStand.Abstraction.Repositories;
using RacksStand.Infrastructure.Persistence.Repositories;
using RacksStand.Infrastructure.Persistence.Contexts;
using RacksStand.Abstraction.Services;
using RacksStand.Infrastructure.Services;
namespace RacksStand.Infrastructure;

public static class DependencyInjection
{

    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<RacksStandContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        return builder;
    }

    public static WebApplicationBuilder AddApplcationRepositories(this WebApplicationBuilder builder)
    {

        return builder;
    }

    public static WebApplicationBuilder AddApplcationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped(typeof(IService<,>), typeof(Service<,>));
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
