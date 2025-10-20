namespace RacksStand.Api.Core.Extensions;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Domain;
using RacksStand.Api.Domain.Interceptors;
using RacksStand.Api.Domain.Repositories;

public static class DataExtensions
{
    public static WebApplicationBuilder AddDatabaseServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Add DbContext with SQL Server provider
        builder.Services.AddSingleton<QueryTimingInterceptor>();
        builder.Services.AddSingleton<AuditEntityInterceptor>();
        builder.Services.AddSingleton<ConnectionLoggingInterceptor>();
        builder.Services.AddSingleton<TransactionLoggingInterceptor>();
        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString,
                npgsqlOptions =>
                {
                    // Apply the retry policy from the configuration.
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(1),
                        errorCodesToAdd: null);
                })
            .AddInterceptors(
                        sp.GetRequiredService<ConnectionLoggingInterceptor>(),
                        sp.GetRequiredService<QueryTimingInterceptor>(),
                        sp.GetRequiredService<AuditEntityInterceptor>(),
                        sp.GetRequiredService<TransactionLoggingInterceptor>())
            .UseSnakeCaseNamingConvention();
        });

        builder.Services.AddDbContextFactory<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString,
                npgsqlOptions =>
                {
                    // Apply the retry policy from the configuration.
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(1),
                        errorCodesToAdd: null);
                })
             .AddInterceptors(
                        sp.GetRequiredService<ConnectionLoggingInterceptor>(),
                        sp.GetRequiredService<QueryTimingInterceptor>(),
                        sp.GetRequiredService<AuditEntityInterceptor>(),
                        sp.GetRequiredService<TransactionLoggingInterceptor>())
            .UseSnakeCaseNamingConvention();
        }, ServiceLifetime.Scoped);

        builder.Services.AddScoped<ITicketStore, PostgresTicketStore>();

        return builder;
    }
}
