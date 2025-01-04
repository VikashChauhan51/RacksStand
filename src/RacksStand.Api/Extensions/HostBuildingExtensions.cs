using RacksStand.Api.Features.Users;
using RacksStand.Api.Middlewares;
using RacksStand.Infrastructure;

namespace RacksStand.Api.Extensions;

public static class HostBuildingExtensions
{
    public static WebApplication AddServices(this WebApplicationBuilder builder)
    {
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
        builder.AddApplicationDbContext();
        builder.AddLoggerServices();

        builder.Services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddAuthorization();
       
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseCors();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication(); 
        app.UseAuthorization();
        app.MapGraphQL();
        app.UseHealthChecks("/healthz");

        return app;
    }
}
