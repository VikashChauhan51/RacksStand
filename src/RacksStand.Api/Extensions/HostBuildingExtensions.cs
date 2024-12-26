using Asp.Versioning;
using Carter;
using RacksStand.Api.Middlewares;
using Scalar.AspNetCore;
using System.Diagnostics;

namespace RacksStand.Api.Extensions;

public static class HostBuildingExtensions
{
    public static WebApplication AddServices(this WebApplicationBuilder builder)
    {
        var assembly = typeof(HostBuildingExtensions).Assembly;
        builder.Services.AddOpenApi();
        builder.AddLogger();
        builder.Services.AddProblemDetails(options =>
        {
            // Customize ProblemDetails
            options.CustomizeProblemDetails = (context) =>
            {
                var problemDetails = context.ProblemDetails;
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;
                problemDetails.Instance = $"{context.HttpContext?.Request.Method} {context.HttpContext?.Request.Path}";
                problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? context.HttpContext?.TraceIdentifier;

            };
        });
        builder.Services.AddCarter();
        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1.0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        builder.Services
            .AddHealthChecks();

        builder.Services.AddValidatorsFromAssembly(assembly);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference(); // scalar/v1
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<ErrorHandlerMiddleware>();

        // Configure the HTTP request pipeline.
        app.NewVersionedApi()
            .MapGroup("api/v{apiVersion:apiVersion}/")
            .HasApiVersion(1)
            .MapCarter();

        app.UseHealthChecks("/healthz");

        return app;
    }
}
