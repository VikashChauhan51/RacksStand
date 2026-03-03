using RacksStands.Framework.Cqrs;
using RacksStands.Framework.Auth;
using RacksStands.Framework.Auth.Authorization;
using RacksStands.Framework.Monitoring.Extensions;
using RacksStands.ApiHost.Extensions;
using RacksStands.Framework.Modules.Bootstrap.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Core framework

builder.Services.AddOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

// Add monitoring
builder.AddRacksStandsMonitoring("RacksStands.ApiHost", "1.0.0");

// Add shared frameworks
builder.Services.AddCqrs();


// Add auth
builder.Services.AddJwtAuthentication("your-issuer", "your-audience", "your-secret-key");
builder.Services.AddDummyAuthorization();

// Load modules dynamically
builder.Services.AddModules(
    builder.Configuration,
    typeof(RacksStands.Module.UserManagement.UserManagementModule)
    );

builder.Services.AddOpenApi();
builder.Services.AddApiVersioning(options => options.ReportApiVersions = true);

builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();

// Global exception handler should be first
app.UseGlobalExceptionHandler();

// Security headers
app.UseSecurityHeaders();

// Idempotency middleware
app.UseIdempotency();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Configure modules
app.UseModules();

app.MapHealthChecks("/health");

app.Run();
