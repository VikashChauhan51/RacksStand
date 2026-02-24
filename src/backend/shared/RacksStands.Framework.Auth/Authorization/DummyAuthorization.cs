using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace RacksStands.Framework.Auth.Authorization;

public static class DummyAuthorizationExtensions
{
    public static IServiceCollection AddDummyAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            // Add more dummy policies
        });

        return services;
    }
}
