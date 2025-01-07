using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;
using ResultifyCore.Outcome;

namespace RacksStand.Api.Features.SignOut;

public class SignoutEndpoint : EndpointBase
{
    public override string RouteName => "signout";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/signout", LogoutAsync)
            .WithName(RouteName)
            .Produces<Outcome<string>>(StatusCodes.Status200OK)
            .Produces<Outcome<string>>(StatusCodes.Status400BadRequest)
            .Produces<Outcome<string>>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Signout endpoint",
                Description = "Logs the user out by revoking the access token and refresh token.",
                Consumer = "Frontend Client, Mobile Application",
                Producer = "Authentication Service"
            })
            .WithOpenApi();
    }

    public async Task<IResult> LogoutAsync
    (
        LogoutRequest request,
        RacksStandContext dbContext,
         IValidator<LogoutRequest> validator,
        ILogger<SignoutEndpoint> logger
    )
    {
        // Validate the request using provided validator
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        // Validate the access token
        var storedAccessToken = await dbContext.UserAccessTokens
            .Where(at => at.Token == request.AccessToken && at.IsActive)
            .FirstOrDefaultAsync();

        if (storedAccessToken == null)
        {
            logger.LogWarning("Invalid or already revoked access token provided.");
            return Results.BadRequest(Outcome<string>.Failure(new OutcomeError("InvalidAccessToken", "The provided access token is invalid or already revoked.")));
        }

        // Validate the refresh token
        var storedRefreshToken = await dbContext.UserRefreshTokens
            .Where(rt => rt.Token == request.RefreshToken && rt.IsActive)
            .FirstOrDefaultAsync();

        if (storedRefreshToken == null)
        {
            logger.LogWarning("Invalid or already revoked refresh token provided.");
            return Results.BadRequest(Outcome<string>.Failure(new OutcomeError("InvalidRefreshToken", "The provided refresh token is invalid or already revoked.")));
        }

        // Check if both tokens belong to the same user
        if (storedAccessToken.UserId != storedRefreshToken.UserId)
        {
            logger.LogWarning("Mismatch between access token and refresh token users.");
            return Results.BadRequest(Outcome<string>.Failure(new OutcomeError("TokenMismatch", "The provided tokens do not match.")));
        }

        dbContext.UserAccessTokens.Remove(storedAccessToken);
        dbContext.UserRefreshTokens.Remove(storedRefreshToken);

        await dbContext.SaveChangesAsync();

        logger.LogInformation("User {UserId} logged out successfully.", storedAccessToken.UserId);

        // Return a success response
        return Results.Ok(Outcome<string>.Success("User logged out successfully."));
    }
}
