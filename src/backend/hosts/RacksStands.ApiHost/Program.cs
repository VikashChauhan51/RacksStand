using RacksStands.Framework.Cqrs;
using RacksStands.Framework.Auth;
using RacksStands.Framework.Auth.Authorization;
using RacksStands.Framework.Infrastructure;
using RacksStands.Framework.Modules.Bootstrap;
using RacksStands.ApiHost.Middleware;
using RacksStands.Module.UserManagement;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add shared frameworks
builder.Services.AddCqrs();
builder.Services.AddOpenTelemetryInfrastructure("RacksStands.ApiHost", "1.0.0");

// Add auth
builder.Services.AddJwtAuthentication("your-issuer", "your-audience", "your-secret-key");
builder.Services.AddDummyAuthorization();

// Load modules dynamically
var modules = LoadModules(builder.Configuration);
foreach (var module in modules)
{
    module.ConfigureServices(builder.Services);
}

// Add API versioning
// builder.Services.AddApiVersioning(options =>
// {
//     options.DefaultApiVersion = new ApiVersion(1, 0);
//     options.AssumeDefaultVersionWhenUnspecified = true;
//     options.ReportApiVersions = true;
// });

builder.Services.AddOpenApi();

// Add memory cache for idempotency
builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Global exception handler should be first
app.UseGlobalExceptionHandler();

// Security headers
app.UseSecurityHeaders();

// Idempotency middleware
app.UseIdempotency();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Configure modules
foreach (var module in modules)
{
    module.ConfigureApplication(app);
}

app.Run();

static IEnumerable<IModule> LoadModules(IConfiguration configuration)
{
    var modules = new List<IModule>();

    // Get all assemblies in the current domain
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.FullName?.Contains("Module") == true);

    foreach (var assembly in assemblies)
    {
        var moduleTypes = assembly.GetTypes()
            .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in moduleTypes)
        {
            // For now, assume constructor takes connection string
            // In a real scenario, you might use DI or configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection")!;
            var module = (IModule)Activator.CreateInstance(type, connectionString)!;
            modules.Add(module);
        }
    }

    return modules;
}
