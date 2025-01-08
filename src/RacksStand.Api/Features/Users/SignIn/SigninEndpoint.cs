using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Api.Core.Infrastructure.Services;
using RacksStand.Domain.Entities;
using RacksStand.Infrastructure.Data.Contexts;
using RacksStand.Infrastructure.Services;
using ResultifyCore.Outcome;

namespace RacksStand.Api.Features.Users.SignIn;

public class SigninEndpoint : EndpointBase
{
    public override string RouteName => "signIn";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/signin", SignInAsync)
            .WithName(RouteName)
            .Produces<Outcome<string>>(StatusCodes.Status200OK)
            .Produces<Outcome<string>>(StatusCodes.Status400BadRequest)
            .Produces<Outcome<string>>(StatusCodes.Status401Unauthorized)
            .Produces<Outcome<string>>(StatusCodes.Status403Forbidden)
            .WithMetadata(new
            {
                Summary = "User sign-in endpoint",
                Description = "Handles user sign-in process, validating credentials and user status.",
                Consumer = "Frontend Client, Mobile Application",
                Producer = "Authentication Service"
            })
            .WithOpenApi();
    }

    public async Task<IResult> SignInAsync
    (
        SigninRequest request,
        IValidator<SigninRequest> validator,
        ILogger<SigninEndpoint> logger,
        RacksStandContext dbContext,
        IJwtService jwtService
    )
    {
        // Validate the request using provided validator
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        // Fetch user by email
        var user = await dbContext.Users
            .Include(x => x.UserPermissions)
            .ThenInclude(x => x.ModulePermission)
            .ThenInclude(x => x.Module)
            .Where(x => x.Email.ToLower() == request.Email.ToLower())
            .FirstOrDefaultAsync();

        // Check if user exists and is active
        if (user == null || !user.IsActive)
        {
            logger.LogWarning("The user not exists for email {Email}", request.Email);
            return Results.NotFound(Outcome<string>.Failure(new OutcomeError("UserNotExists", "The User does not exist.")));
        }

        // Check if the email is confirmed
        if (!user.IsEmailConfirmed)
        {
            logger.LogWarning("The user account is not confirmed for email {Email}", request.Email);
            return Results.BadRequest(Outcome<string>.Failure(new OutcomeError("UserAccountNotConfirmed", "The User account is not confirmed.")));
        }

        // Check if the account is locked
        if (user.LockoutEnabled && user.LockoutEnd > DateTimeOffset.UtcNow)
        {
            logger.LogWarning("The user account is locked for email {Email}", request.Email);
            return Results.Forbid();
        }

        // Check password hash
        if (!PasswordHasher.VerifyHashedPassword(user.PasswordHash, request.Password))
        {
            // Increment failed attempts and lock user if necessary
            user.AccessFailedCount++;
            if (user.AccessFailedCount >= 3)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.UtcNow.AddHours(1);
            }
            dbContext.Users.Update(user); // Update user in the database
            await dbContext.SaveChangesAsync();
            return Results.BadRequest(Outcome<string>.Failure(new OutcomeError("InvalidCredentials", "Invalid credentials provided.")));
        }
        else
        {
            // Reset lockout and failed count
            user.AccessFailedCount = 0;
            user.LockoutEnabled = false;
            user.LockoutEnd = null;
            dbContext.Users.Update(user); // Update user in the database
            await dbContext.SaveChangesAsync();
        }

        // Prepare and return successful outcome
        var accessToken = jwtService.GenerateAccessToken(user);
        var refreshToken = jwtService.GenerateRefreshToken();

        var userAccessToken = new UserAccessToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            Token = accessToken,
            CreatedAt = DateTimeOffset.UtcNow,
            ExpirationDate = DateTimeOffset.UtcNow.AddHours(24),
            IsActive = true
        };

        var userRefreshToken = new UserRefreshToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            Token = refreshToken,
            CreatedAt = DateTimeOffset.UtcNow,
            ExpirationDate = DateTimeOffset.UtcNow.AddDays(30),
            IsActive = true
        };

        dbContext.UserAccessTokens.Add(userAccessToken);
        dbContext.UserRefreshTokens.Add(userRefreshToken);
        await dbContext.SaveChangesAsync();
        return Results.Ok(new
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken

        });
    }
}


