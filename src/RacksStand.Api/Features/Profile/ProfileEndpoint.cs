using RacksStand.Api.Core;
using RacksStand.Api.Features.ConfirmAccount;
using RacksStand.Infrastructure.Data.Contexts;
using ResultifyCore.Outcome;
using System.Security.Claims;

namespace RacksStand.Api.Features.Profile;

public class ProfileEndpoint : EndpointBase
{
    public ProfileEndpoint()
    {
        this.RequireAuthorization();
    }
    public override string RouteName => "profile";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/profile", ProfileAsync)
            .WithName(RouteName)
            .Produces<Outcome<ProfileResponse>>(StatusCodes.Status200OK)
            .Produces<Outcome<ProfileResponse>>(StatusCodes.Status400BadRequest)
            .Produces<Outcome<ProfileResponse>>(StatusCodes.Status404NotFound)
            .WithMetadata(new
            {
                Summary = "User Profile Endpoint",
                Description = "Get a user's profile.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> ProfileAsync(
        ILogger<ProfileEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext)
    {
        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Results.Unauthorized();
        }

        var user = await dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            logger.LogWarning("User not found for NameIdentifier clam: {Identifier}", userId);
            return Results.NotFound(Outcome<ProfileResponse>.Failure(new OutcomeError("UserNotFound", "The user associated with this token does not exist.")));
        }
        return Results.Ok(Outcome<ProfileResponse>.Success(new ProfileResponse
        (
            Id: user.Id.Value,
            FullName: user.FullName,
            Email: user.Email,
            Phone: user.PhoneNumber,
            Theme: user.Theme,
            Local: user.Local,
            IsEmailConfirmed: user.IsEmailConfirmed,
            IsPhoneNumberConfirmed: user.PhoneNumberConfirmed,
            CompanyId: user.SelectedCompanyId.Value
        )));
    }
}
