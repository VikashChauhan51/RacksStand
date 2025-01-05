using Carter;
using Microsoft.Extensions.DependencyInjection;
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
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();
        builder.Services.AddCarter();
        builder.AddApplicationDbContext();
        builder.AddLoggerServices();
        builder.Services.AddValidatorsFromAssembly(typeof(HostBuildingExtensions).Assembly);

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
        app.MapCarter();
        app.UseHealthChecks("/healthz");

        return app;
    }
}
