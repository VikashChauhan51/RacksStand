using RacksStands.Framework.Cqrs;
using RacksStands.Framework.Auth;
using RacksStands.Framework.Auth.Authorization;
using RacksStands.Framework.Infrastructure;
using RacksStands.Module.UserManagement;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add shared frameworks
builder.Services.AddCqrs();
builder.Services.AddOpenTelemetryInfrastructure("RacksStands.ApiHost", "1.0.0");

// Add auth
builder.Services.AddJwtAuthentication("your-issuer", "your-audience", "your-secret-key");
builder.Services.AddDummyAuthorization();

// Add modules
builder.Services.AddUserManagementModule(builder.Configuration.GetConnectionString("DefaultConnection")!);

// Add API versioning
// builder.Services.AddApiVersioning(options =>
// {
//     options.DefaultApiVersion = new ApiVersion(1, 0);
//     options.AssumeDefaultVersionWhenUnspecified = true;
//     options.ReportApiVersions = true;
// });

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Use modules
app.UseUserManagementModule();

app.Run();
