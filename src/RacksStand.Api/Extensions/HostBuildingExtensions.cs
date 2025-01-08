using Asp.Versioning;
using Carter;
using RacksStand.Api.Core.Infrastructure.Services;
using RacksStand.Api.Middlewares;
using RacksStand.Infrastructure;
using Scalar.AspNetCore;

namespace RacksStand.Api.Extensions;

public static class HostBuildingExtensions
{
    public static WebApplication AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services
            .AddHealthChecks();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();
        builder.Services.AddCarter();
        builder.AddApplicationDbContext();
        builder.AddLoggerServices();
        builder.Services.AddValidatorsFromAssembly(typeof(HostBuildingExtensions).Assembly);
        builder.Services.AddSingleton<IJwtService, JwtService>();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.MapOpenApi();
        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference(opt =>
            {
                opt.Title = "Racks Stand";
                opt.Theme = ScalarTheme.Mars;
                opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
            });
        }
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseCors();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.NewVersionedApi()
            .MapGroup("api/v{apiVersion:apiVersion}/")
            .HasApiVersion(1)
            .MapCarter();
        app.UseHealthChecks("/healthz");

        return app;
    }
}
