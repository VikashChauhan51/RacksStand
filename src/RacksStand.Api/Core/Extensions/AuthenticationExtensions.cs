namespace RacksStand.Api.Core.Extensions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using RacksStand.Api.Core.Configurations;


public static class AuthenticationExtensions
{
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AuthenticationConfiguration>(
        builder.Configuration.GetSection("Authentication"));
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
        builder.Services.AddAuthorization();
    }
}
