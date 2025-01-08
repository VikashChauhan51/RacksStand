using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;
using ResultifyCore.Outcome;

namespace RacksStand.Api.Features.Profile;

public class ProfileEndpoint : EndpointBase
{
    public ProfileEndpoint()
    {
        this.RequireAuthorization();
    }
    public override string RouteName => "getProfile";
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
        var userName = httpContext.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            return Results.Unauthorized();
        }

        var user = await dbContext.Users
            .AsNoTracking().
            Where(x => x.UserName == userName)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            logger.LogWarning("User not found for username clam: {UserName}", userName);
            return Results.NotFound(Outcome<ProfileResponse>.Failure(new OutcomeError("UserNotFound", "The user associated with this token does not exist.")));
        }
        var companyName = await dbContext.Companies
            .AsNoTracking()
            .Where(x => x.Id == user.SelectedCompanyId)
            .Select(x => x.Name)
            .FirstAsync();

        return Results.Ok(Outcome<ProfileResponse>.Success(new ProfileResponse
        (
            FullName: user.FullName,
            Email: user.Email,
            Phone: user.PhoneNumber,
            Theme: user.Theme,
            Local: user.Local,
            IsEmailConfirmed: user.IsEmailConfirmed,
            IsPhoneNumberConfirmed: user.PhoneNumberConfirmed,
            CompanyName: companyName
        )));
    }
}
