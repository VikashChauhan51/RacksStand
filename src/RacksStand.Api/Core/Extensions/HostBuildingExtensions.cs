namespace RacksStand.Api.Core.Extensions;

using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.DataProtection;
using RacksStand.Api.Core.Middlewares;
using RacksStand.Api.Domain;

public static class HostBuildingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddOptions();
        builder.ConfigureCache();
        builder.ConfigureOpenTelemetry();
        builder.Services.AddCors();
        builder.ConfigureAuthentication();
        builder.Services.AddOpenApi();
        builder.Services.SwaggerDocument(o =>
        {
            o.MaxEndpointVersion = 1;
            o.DocumentSettings = s =>
            {
                s.DocumentName = "Release 1";
                s.Title = "Racks Stand Api";
                s.Version = "v1";
            };
        });
        builder.Services.AddFastEndpoints()
            .AddSwaggerDocument();

        builder.AddDatabaseServices();
        builder.Services.AddDataProtection(options =>
        {
            options.ApplicationDiscriminator = "RacksStand.Api";
        }).PersistKeysToDbContext<ApplicationDbContext>();

        //Add health checks
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!, name: "SQL Server");

        return builder.Build();
    }
    public static WebApplication ConfigurePipelines(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseMiddleware<LoggingEnrichmentMiddleware>();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<UserContextEnrichmentMiddleware>();
        app.UseFastEndpoints(c =>
        {
            c.Errors.UseProblemDetails();
            c.Versioning.Prefix = "v";
            c.Versioning.DefaultVersion = 1;
            c.Versioning.PrependToRoute = true;
        }).UseSwaggerGen();

        app.MapHealthChecks("/health")
            .AllowAnonymous()
            .DisableHttpMetrics()
            .DisableRateLimiting();

        return app;
    }
    }
