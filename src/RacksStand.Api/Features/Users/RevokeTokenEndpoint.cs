using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;
using ResultifyCore.Outcome;

namespace RacksStand.Api.Features.Users;

public class RevokeTokenEndpoint : EndpointBase
{
    public override string RouteName => "revokeToken";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/revoke-token", RevokeTokenAsync)
            .WithName(RouteName)
            .Produces<Outcome<string>>(StatusCodes.Status200OK)
            .Produces<Outcome<string>>(StatusCodes.Status400BadRequest)
            .Produces<Outcome<string>>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Revoke token endpoint",
                Description = "Revokes both access token and refresh token for a user.",
                Consumer = "Frontend Client, Mobile Application",
                Producer = "Authentication Service"
            })
            .WithOpenApi();
    }

    public async Task<IResult> RevokeTokenAsync
    (
        [FromQuery] string token,
        RacksStandContext dbContext,
        ILogger<RevokeTokenEndpoint> logger
    )
    {
        if (string.IsNullOrEmpty(token))
        {
            return Results.BadRequest("Invalid token");
        }
        // Validate the access token
        var storedAccessToken = await dbContext.UserAccessTokens
            .Where(at => at.Token == token && at.IsActive)
            .FirstOrDefaultAsync();

        if (storedAccessToken == null)
        {
            logger.LogWarning("Invalid or already revoked access token provided.");
            return Results.BadRequest(Outcome<string>.Failure(new OutcomeError("InvalidAccessToken", "The provided access token is invalid or already revoked.")));
        }

        // Revoke both tokens
        dbContext.UserAccessTokens.Remove(storedAccessToken);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Access and refresh tokens successfully revoked for user {UserId}.", storedAccessToken.UserId);

        // Return a success response
        return Results.Ok(Outcome<string>.Success("Tokens revoked successfully."));
    }
}
