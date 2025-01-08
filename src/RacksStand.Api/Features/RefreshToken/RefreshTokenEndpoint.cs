using RacksStand.Api.Core.Infrastructure.Services;
using RacksStand.Api.Core;
using RacksStand.Domain.Entities;
using RacksStand.Infrastructure.Data.Contexts;
using ResultifyCore.Outcome;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RacksStand.Api.Features.RefreshToken;

public class RefreshTokenEndpoint : EndpointBase
{
    public override string RouteName => "refreshToken";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/refresh-token", RefreshTokenAsync)
            .WithName(RouteName)
            .Produces<Outcome<string>>(StatusCodes.Status200OK)
            .Produces<Outcome<string>>(StatusCodes.Status400BadRequest)
            .Produces<Outcome<string>>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Refresh token endpoint",
                Description = "Generates a new access token and refresh token using a valid refresh token.",
                Consumer = "Frontend Client, Mobile Application",
                Producer = "Authentication Service"
            })
            .WithOpenApi();
    }

    public async Task<IResult> RefreshTokenAsync
    (
       [FromQuery] string token,
        RacksStandContext dbContext,
        IJwtService jwtService,
        ILogger<RefreshTokenEndpoint> logger
    )
    {

        if (string.IsNullOrEmpty(token))
        {
            return Results.BadRequest("Invalid token");
        }
        // Validate the refresh token
        var storedRefreshToken = await dbContext.UserRefreshTokens
            .Where(rt => rt.Token == token && rt.IsActive && rt.ExpirationDate > DateTimeOffset.UtcNow)
            .FirstOrDefaultAsync();

        if (storedRefreshToken == null)
        {
            logger.LogWarning("Invalid or expired refresh token provided.");
            return Results.Unauthorized();
        }

        // Fetch associated user
        var user = await dbContext.Users
            .Where(u => u.Id == storedRefreshToken.UserId && u.IsActive)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            logger.LogWarning("No active user associated with the provided refresh token.");
            return Results.Unauthorized();
        }

        // Invalidate the old refresh token
        storedRefreshToken.IsActive = false;
        dbContext.UserRefreshTokens.Update(storedRefreshToken);

        // Generate new tokens
        var newAccessToken = jwtService.GenerateAccessToken(user);
        var newRefreshToken = jwtService.GenerateRefreshToken();

        // Save new tokens
        var newUserAccessToken = new UserAccessToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            Token = newAccessToken,
            CreatedAt = DateTimeOffset.UtcNow,
            ExpirationDate = DateTimeOffset.UtcNow.AddHours(24),
            IsActive = true
        };

        var newUserRefreshToken = new UserRefreshToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            Token = newRefreshToken,
            CreatedAt = DateTimeOffset.UtcNow,
            ExpirationDate = DateTimeOffset.UtcNow.AddDays(30),
            IsActive = true
        };

        dbContext.UserAccessTokens.Add(newUserAccessToken);
        dbContext.UserRefreshTokens.Add(newUserRefreshToken);
        await dbContext.SaveChangesAsync();

        // Return the new tokens
        return Results.Ok(new
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }
}
