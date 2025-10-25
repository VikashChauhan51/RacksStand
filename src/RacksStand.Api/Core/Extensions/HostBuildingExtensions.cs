namespace RacksStand.Api.Core.Extensions;

using FastEndpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using RacksStand.Api.Core.Authentication;
using RacksStand.Api.Core.Authentication.Abstractions;
using RacksStand.Api.Core.Caching;
using RacksStand.Api.Core.Caching.Validators;
using RacksStand.Api.Core.Configurations;
using RacksStand.Api.Core.Configurations.Validators;
using RacksStand.Api.Core.Emails;
using RacksStand.Api.Core.Sms;
using RacksStand.Api.Core.Hashers;
using RacksStand.Api.Core.Logging;
using RacksStand.Api.Core.Middlewares;
using RacksStand.Api.Core.Resilience;
using RacksStand.Api.Core.Resilience.Configurations;
using RacksStand.Api.Core.Resilience.Configurations.Validators;
using RacksStand.Api.Core.Serializers;
using RacksStand.Api.Domain;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Domain.Interceptors;
using RacksStand.Api.Domain.Repositories;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;
using RacksStand.Api.Domain.Repositories.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public static class HostBuildingExtensions
{


    /// <summary>
    /// Configure all required services and build the WebApplication.
    /// </summary>
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // 1️⃣ Core services
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddOptions();

        // 2️⃣ Configure caching early for DI usage
        builder.ConfigureCache();

        // 3️⃣ OpenTelemetry instrumentation (logs, metrics, traces)
        builder.ConfigureOpenTelemetry();
        builder.ConfigurePolly();

        // 4️⃣ CORS
        builder.ConfigureCors();

        // 5️⃣ Authentication & Identity
        builder.ConfigureAuthentication();
        builder.ConfigureAuthorization();

        // 6️⃣ FastEndpoints and Swagger
        builder.Services.AddFastEndpoints();
        builder.Services.AddOpenApi();

        // 7️⃣ Data protection
        builder.Services.AddDataProtection(options =>
        {
            options.ApplicationDiscriminator = "RacksStand.Api";
        }).PersistKeysToDbContext<ApplicationDbContext>();

        // 8️⃣ Hasher service
    builder.Services.AddSingleton<IHasher, Hasher>();
    builder.Services.AddTransient<IEmailSender, DummyEmailSender>();
    builder.Services.AddTransient<ISmsSender, DummySmsSender>();

        // 9️⃣ FluentValidation - scan assembly containing your validators
        builder.Services.AddValidatorsFromAssemblyContaining<AuthenticationConfigurationValidator>();

        // 🔟 Health checks
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!,
                name: "SQL Server");

        // 1️⃣1️⃣ Database & repositories
        builder.AddDatabaseServices();

        // ✅ Finally build the WebApplication
        return builder.Build();
    }

    /// <summary>
    /// Configure the middleware pipeline.
    /// </summary>
    public static WebApplication ConfigurePipelines(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // 1️⃣ Custom error handling middleware
        app.UseMiddleware<ErrorHandlerMiddleware>();

        // 2️⃣ Logging enrichment middleware
        app.UseMiddleware<LoggingEnrichmentMiddleware>();

        // 3️⃣ CORS policy
        app.UseCors();

        // 4️⃣ Authentication & Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // 5️⃣ User context enrichment
        app.UseMiddleware<UserContextEnrichmentMiddleware>();

        // 6️⃣ FastEndpoints pipeline with versioning
        app.UseFastEndpoints(c =>
        {
            c.Errors.UseProblemDetails();
            c.Versioning.Prefix = "v";
            c.Versioning.DefaultVersion = 1;
            c.Versioning.PrependToRoute = true;
        });

        // 7️⃣ Health check endpoint
        app.MapHealthChecks("/health")
            .AllowAnonymous()
            .DisableHttpMetrics()
            .DisableRateLimiting();

        return app;
    }

    /// <summary>
    /// Adds DbContext, interceptors, and repositories.
    /// </summary>
    private static WebApplicationBuilder AddDatabaseServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // 1️⃣ Add interceptors as singleton
        builder.Services.AddSingleton<QueryTimingInterceptor>();
        builder.Services.AddSingleton<AuditEntityInterceptor>();
        builder.Services.AddSingleton<ConnectionLoggingInterceptor>();
        builder.Services.AddSingleton<TransactionLoggingInterceptor>();

        // 2️⃣ Add DbContext
        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: Constants.MaxRetryCount,
                    maxRetryDelay: TimeSpan.FromMilliseconds(Constants.RetryDelayMilliseconds),
                    errorCodesToAdd: null);
            })
            .AddInterceptors(
                sp.GetRequiredService<ConnectionLoggingInterceptor>(),
                sp.GetRequiredService<QueryTimingInterceptor>(),
                sp.GetRequiredService<AuditEntityInterceptor>(),
                sp.GetRequiredService<TransactionLoggingInterceptor>())
            .UseSnakeCaseNamingConvention();
        });

        // 3️⃣ Add DbContextFactory for scoped usage
        builder.Services.AddDbContextFactory<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: Constants.MaxRetryCount,
                    maxRetryDelay: TimeSpan.FromMilliseconds(Constants.RetryDelayMilliseconds),
                    errorCodesToAdd: null);
            })
            .AddInterceptors(
                sp.GetRequiredService<ConnectionLoggingInterceptor>(),
                sp.GetRequiredService<QueryTimingInterceptor>(),
                sp.GetRequiredService<AuditEntityInterceptor>(),
                sp.GetRequiredService<TransactionLoggingInterceptor>())
            .UseSnakeCaseNamingConvention();
        }, ServiceLifetime.Scoped);

        // 4️⃣ Add repositories
        builder.Services.AddScoped<ITicketStore, PostgresTicketStore>();
        builder.Services.AddScoped<IAccessTokenReadRepository, AccessTokenReadRepository>();
        builder.Services.AddScoped<IRefreshTokenReadRepository, RefreshTokenReadRepository>();
        builder.Services.AddScoped<IAccessTokenWriteRepository, AccessTokenWriteRepository>();
        builder.Services.AddScoped<IRefreshTokenWriteRepository, RefreshTokenWriteRepository>();

        return builder;
    }

    /// <summary>
    /// Configure Authentication, Identity, and JWT bearer tokens.
    /// </summary>
    private static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        // 1️⃣ Configure Authentication options with FluentValidation
        builder.Services.AddOptions<AuthenticationConfiguration>()
            .Bind(builder.Configuration.GetSection(Constants.AuthenticationSectionName))
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
                        result.Errors.Select(e => e.ErrorMessage));
                }

                return true;
            })
            .ValidateOnStart();

        // 2️⃣ Identity
        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // 3️⃣ JWT Bearer Authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Headers.TryGetValue(Constants.AuthorizationHeader, out var authHeader))
                        {
                            var token = authHeader.FirstOrDefault()?.Split(" ").Last();
                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                            }
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = async context =>
                    {
                        var validator = context.HttpContext.RequestServices.GetRequiredService<IAccessTokenValidator>();
                        if (validator == null)
                        {
                            context.Fail("Access token validator not available.");
                            return;
                        }

                        var token = context.SecurityToken as JwtSecurityToken;
                        if (token == null)
                        {
                            context.Fail("Invalid token");
                            return;
                        }
                        var principal = await validator.ValidateAsync(token.RawData);
                        if (principal == null)
                        {
                            context.Fail("Token is invalid or revoked");
                            return;
                        }

                        context.Principal = principal;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }
                };
            });
        builder.Services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IAccessTokenValidator, AccessTokenValidator>();
    }

    /// <summary>
    /// Configure Authorization.
    /// </summary>
    /// <param name="builder"></param>
    private static void ConfigureAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {

            options.AddPolicy("AdminOnly", policy =>
            {
                policy.RequireRole("Admin");
            });

            options.AddPolicy("EmailVerified", policy =>
            {
                policy.RequireClaim("email_verified", "true");
            });
        });
    }

    /// <summary>
    /// Configure Cors.
    /// </summary>
    /// <param name="builder"></param>
    private static void ConfigureCors(this WebApplicationBuilder builder)
    {
        var allowedOrigins = builder.Configuration
            .GetSection(Constants.CorsSectionName)
            .Get<string[]>() ?? Array.Empty<string>();

        builder.Services.AddCors(options =>
        {
            // Default policy
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });

            // Optional: Named policy with credentials
            options.AddPolicy(Constants.CorsAllowAllWithCredentialsName, policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });

            // Optional: Open policy for internal testing
            options.AddPolicy(Constants.CorsOpenPolicyName, policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

    }


    /// <summary>
    /// Configure HybridCache
    /// </summary>
    private static void ConfigureCache(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOptions<HybridCacheEntryOptions>()
            .Bind(builder.Configuration.GetSection(Constants.CachingSectionName))
            .Validate(config =>
            {
                var validator = new HybridCacheEntryOptionsValidator();
                var result = validator.Validate(config);

                if (!result.IsValid)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    throw new OptionsValidationException(
                        nameof(HybridCacheEntryOptions),
                        typeof(HybridCacheEntryOptions),
                        result.Errors.Select(e => e.ErrorMessage)
                    );
                }

                return true;
            }).ValidateOnStart();


        builder.Services.AddHybridCache(options =>
        {
            var cacheOptions = new HybridCacheEntryOptions();
            builder.Configuration.GetSection(Constants.CachingSectionName).Bind(cacheOptions);
            options.MaximumPayloadBytes = Constants.MaxPayloadSizeBytes;
            options.MaximumKeyLength = Constants.MaximumKeyLength;
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = cacheOptions?.Expiration ?? TimeSpan.FromMinutes(Constants.DefaultCacheExpirationMinutes),
                LocalCacheExpiration = cacheOptions?.LocalCacheExpiration ?? TimeSpan.FromMinutes(Constants.DefaultCacheExpirationMinutes)
            };
        });

        builder.Services.AddSingleton<ICacheProvider, HybridCacheProvider>();
        builder.Services.AddSingleton<IEntitySerializer, JsonEntitySerializer>();
    }

    /// <summary>
    /// Configure OpenTelemetry instrumentation for logging, metrics, and traces.
    /// </summary>
    private static void ConfigureOpenTelemetry(this WebApplicationBuilder builder)
    {
        // 1️⃣ Logging
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
            logging.ParseStateValues = true;
            logging.AddProcessor(new AttributesPrefixProcessor());
        });

        // 2️⃣ Metrics & Tracing
        var otel = builder.Services.AddOpenTelemetry();

        otel.WithMetrics(metrics =>
        {
            metrics.AddAspNetCoreInstrumentation();
            metrics.AddSqlClientInstrumentation();
            metrics.AddHttpClientInstrumentation();
            metrics.AddRuntimeInstrumentation();
            metrics.AddMeter("Microsoft.AspNetCore.Hosting");
            metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
        });

        otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation();
            tracing.AddHttpClientInstrumentation();
            tracing.AddSqlClientInstrumentation();
            tracing.AddEntityFrameworkCoreInstrumentation();
            tracing.AddProcessor<HttpContextEnrichmentProcessor>();
        });

        // 3️⃣ Export via OTLP
        otel.UseOtlpExporter();
    }


    /// <summary>
    ///  Configure polly.
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="OptionsValidationException"></exception>
    private static void ConfigurePolly(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<PolyOptions>()
            .Bind(builder.Configuration.GetSection(Constants.PollySectionName))
            .Validate(options =>
            {
                var validator = new PolyOptionsValidator();
                var result = validator.Validate(options);

                if (!result.IsValid)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    throw new OptionsValidationException(
                        nameof(PolyOptions),
                        typeof(PolyOptions),
                        result.Errors.Select(e => e.ErrorMessage)
                    );
                }

                return true;
            }).ValidateOnStart();

        builder.Services.AddSingleton<IPolicyService, PolicyService>();

    }
}

