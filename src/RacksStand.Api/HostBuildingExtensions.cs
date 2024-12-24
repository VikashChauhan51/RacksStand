namespace RacksStand.Api;

public static class HostBuildingExtensions
{

    public static WebApplication AddServices(this WebApplicationBuilder builder)
    {

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

        return app;
    }

    public static void SafeRun(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        try
        {
            app.Run();
        }
        catch (Exception ex)
        {
            logger?.LogCritical(ex, "Host terminated");
            throw;
        }
    }
}
