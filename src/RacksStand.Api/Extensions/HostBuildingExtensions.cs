using Asp.Versioning;
using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RacksStand.Api.Core;
using RacksStand.Api.Middlewares;
using RacksStand.Infrastructure;
using Scalar.AspNetCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace RacksStand.Api.Extensions;

public static class HostBuildingExtensions
{
    public static WebApplication AddServices(this WebApplicationBuilder builder)
    {
        var assembly = typeof(HostBuildingExtensions).Assembly;
        builder.Services.AddOpenApi();
        builder.AddLoggerServices();
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
        builder
            .AddDbContext();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
        // Add Authentication with JWT Bearer
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new { error = "Unauthorized access" });
                    return context.Response.WriteAsync(result);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new { error = "Forbidden" });
                    return context.Response.WriteAsync(result);
                },
                OnTokenValidated = context =>
                {
                    // Custom logic here, e.g., checking revoked tokens
                    var userId = context!.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (string.IsNullOrEmpty(userId))
                    {
                        context!.Fail("Unauthorized: Missing user ID.");
                    }

                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                },

            };

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

        builder.Services.AddAuthorization();
        RegisterFeatureModules.RegisterModules(builder.Services, builder.Configuration, assembly);

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
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseCors("AllowAll");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        // Configure the HTTP request pipeline.
        app.NewVersionedApi()
            .MapGroup("api/v{apiVersion:apiVersion}/")
            .HasApiVersion(1)
            .MapCarter();

        app.UseHealthChecks("/healthz");

        return app;
    }
}
