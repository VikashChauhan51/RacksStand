using Carter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;
using ResultifyCore.Outcome;

namespace RacksStand.Api.Features.ConfirmAccount;

public class AccountConfirmationEndpoint : EndpointBase
{
    public override string RouteName => "confirmAccount";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/confirm-account", ConfirmAccountAsync)
            .WithName(RouteName)
            .Produces<Outcome<string>>(StatusCodes.Status200OK)
            .Produces<Outcome<string>>(StatusCodes.Status400BadRequest)
            .Produces<Outcome<string>>(StatusCodes.Status404NotFound)
            .WithMetadata(new
            {
                Summary = "Account Confirmation Endpoint",
                Description = "Confirms a user's account using a token sent to their email.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();
    }

    private static async Task<IResult> ConfirmAccountAsync(
        [FromQuery] string token,
        ILogger<AccountConfirmationEndpoint> logger,
        RacksStandContext dbContext)
    {
        if (string.IsNullOrEmpty(token))
        {
            return Results.BadRequest("Invalid token");
        }

        // Find the confirmation token in the database
        var confirmation = await dbContext.UserAccountConfirmations
            .Where(x => x.Token == token && x.ExpiresAt > DateTime.UtcNow)
            .FirstOrDefaultAsync();

        if (confirmation == null)
        {
            logger.LogWarning("Invalid or expired confirmation token: {Token}", token);
            return Results.BadRequest(Outcome<string>.Failure(new OutcomeError("InvalidToken", "The confirmation token is invalid or expired.")));
        }

        // Find the associated user
        var user = await dbContext.Users.FindAsync(confirmation.UserId);
        if (user == null)
        {
            logger.LogWarning("User not found for confirmation token: {Token}", token);
            return Results.NotFound(Outcome<string>.Failure(new OutcomeError("UserNotFound", "The user associated with this token does not exist.")));
        }

        // Confirm the user's email
        user.IsEmailConfirmed = true;
        dbContext.Users.Update(user);

        // Mark the confirmation token as used
        dbContext.UserAccountConfirmations.Remove(confirmation);

        await dbContext.SaveChangesAsync();

        logger.LogInformation("Account confirmed successfully for user: {UserId}", user.Id);
        return Results.Ok(Outcome<string>.Success("Account confirmed successfully."));
    }
}
