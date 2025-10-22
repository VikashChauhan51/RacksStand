namespace RacksStand.Api.Core.Extensions;

using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using RacksStand.Api.Core.Caching;
using RacksStand.Api.Core.Configurations;
using RacksStand.Api.Core.Configurations.Validators;
using RacksStand.Api.Core.Hashers;
using RacksStand.Api.Core.Logging;
using RacksStand.Api.Core.Middlewares;
using RacksStand.Api.Core.Serializers;
using RacksStand.Api.Domain;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Domain.Interceptors;
using RacksStand.Api.Domain.Repositories;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;
using RacksStand.Api.Domain.Repositories.Tokens;
using System.Text;

public static class HostBuildingExtensions
{
    private const string AuthenticationSectionName = "Authentication";
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

        // Added data protactor services
        builder.Services.AddDataProtection(options =>
        {
            options.ApplicationDiscriminator = "RacksStand.Api";
        }).PersistKeysToDbContext<ApplicationDbContext>();

        // Add haser service
        builder.Services.AddSingleton<IHasher, Hasher>();

        // Add validators
        builder.Services.AddValidatorsFromAssemblyContaining<AuthenticationConfigurationValidator>();
        //Add health checks
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!, name: "SQL Server");

        // Add database services
        builder.AddDatabaseServices();

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

    private static WebApplicationBuilder AddDatabaseServices(this WebApplicationBuilder builder)
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

        // Add repositories
        builder.Services.AddScoped<ITicketStore, PostgresTicketStore>();
        builder.Services.AddScoped<IAccessTokenReadRepository, AccessTokenReadRepository>();
        builder.Services.AddScoped<IRefreshTokenReadRepository, RefreshTokenReadRepository>();

        return builder;
    }
    private static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
     .AddOptions<AuthenticationConfiguration>()
     .Bind(builder.Configuration.GetSection(AuthenticationSectionName))
     .Validate(config =>
     {
         var validator = new AuthenticationConfigurationValidator();
         var result = validator.Validate(config);

         if (!result.IsValid)
         {
             var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
             throw new OptionsValidationException(
                 nameof(AuthenticationConfiguration),
                 typeof(AuthenticationConfiguration),
                 result.Errors.Select(e => e.ErrorMessage)
             );
         }

         return true;
     })
     .ValidateOnStart();



        // Identity
        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                AuthenticationConfiguration authConfig = new();
                builder.Configuration.GetSection(AuthenticationSectionName).Bind(authConfig);
                var key = Encoding.UTF8.GetBytes(authConfig.JwtSecret);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authConfig.Issuer,
                    ValidAudience = authConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddAuthorization();
    }

    private static void ConfigureCache(this WebApplicationBuilder builder)
    {
        var cacheOptions = new HybridCacheEntryOptions();
        builder.Configuration.GetSection("Caching").Bind(cacheOptions);

        builder.Services.AddHybridCache(options =>
        {
            options.MaximumPayloadBytes = 1024 * 1024;
            options.MaximumKeyLength = 1024;
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = cacheOptions?.Expiration ?? TimeSpan.FromMinutes(5),
                LocalCacheExpiration = cacheOptions?.LocalCacheExpiration ?? TimeSpan.FromMinutes(5)
            };
        });
        builder.Services.AddSingleton<ICacheProvider, HybridCacheProvider>();
        builder.Services.AddSingleton<IEntitySerializer, JsonEntitySerializer>();
    }


    private static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
    {
        // Setup logging to be exported via OpenTelemetry
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
            logging.AddProcessor(new AttributesPrefixProcessor());
        });

        var otel = builder.Services.AddOpenTelemetry();

        // Add Metrics for ASP.NET Core and our custom metrics and export via OTLP
        otel.WithMetrics(metrics =>
        {
            // Metrics provider from OpenTelemetry
            metrics.AddAspNetCoreInstrumentation();
            metrics.AddSqlClientInstrumentation();
            metrics.AddHttpClientInstrumentation();
            metrics.AddRuntimeInstrumentation();
            // Metrics provides by ASP.NET Core in .NET 8
            metrics.AddMeter("Microsoft.AspNetCore.Hosting");
            metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
        });

        // Add Tracing for ASP.NET Core and our custom ActivitySource and export via OTLP
        otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();
            tracing.AddSqlClientInstrumentation();
            tracing.AddEntityFrameworkCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();
            tracing.AddProcessor<HttpContextEnrichmentProcessor>();
        });

        otel.UseOtlpExporter();
    }
}
