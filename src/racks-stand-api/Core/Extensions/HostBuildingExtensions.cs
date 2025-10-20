using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Identity.Web;
using RacksStand.Api.Core.Middlewares;
using RacksStand.Api.Domain;

namespace RacksStand.Api.Core.Extensions;

public static class HostBuildingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddOptions();

        builder.Services.AddCors();
        // Add services to the container.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        //builder.AddDatabaseServices();
        //builder.Services.AddDataProtection(options =>
        //{
        //    options.ApplicationDiscriminator = "RacksStand.Api";
        //}).PersistKeysToDbContext<ApplicationDbContext>();

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
        return app;
    }
    }
