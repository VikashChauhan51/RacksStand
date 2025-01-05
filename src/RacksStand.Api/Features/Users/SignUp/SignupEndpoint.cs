using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Domain.Entities;
using RacksStand.Infrastructure.Data.Contexts;
using RacksStand.Infrastructure.Services;
using ResultifyCore.Outcome;

namespace RacksStand.Api.Features.Users.SignUp;

public class SignUpEndpoint : EndpointBase
{
    public override string RouteName => "signUp";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/signup", SignUpAsync)
            .WithName(RouteName)
            .Produces<Outcome<string>>(StatusCodes.Status200OK)
            .Produces<Outcome<string>>(StatusCodes.Status400BadRequest)
            .Produces<Outcome<string>>(StatusCodes.Status500InternalServerError)
            .WithMetadata(new
            {
                Summary = "User sign-up endpoint",
                Description = "Handles user sign-up process, validating input and creating a new user.",
                Consumer = "Frontend Client, Mobile Application",
                Producer = "Authentication Service"
            });
    }

    public async Task<IResult> SignUpAsync
    (
        SignupRequest request,
        IValidator<SignupRequest> validator,
        ILogger<SignUpEndpoint> logger,
        RacksStandContext dbContext
    )
    {
        // Validate the request using provided validator
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        // Check if user already exists
        var isUserExists = await dbContext.Users.AsNoTracking()
            .Where(x => x.Email.ToLower() == request.Email.ToLower())
            .AnyAsync();

        if (isUserExists)
        {
            logger.LogWarning("The user already exists for email {Email}", request.Email);
            return Results.BadRequest(Outcome<string>.Failure(new OutcomeError("UserExists", "The User already exists.")));
        }

        // Fetch plan and permissions
        var plan = await dbContext.Plans.AsNoTracking()
            .Where(x => x.DurationInMonths == 1)
            .FirstAsync();

        var permissions = await dbContext.Permissions.AsNoTracking()
            .ToListAsync();

        // Hash the password
        var passwordHash = PasswordHasher.HashPassword(request.Password);

        // Create the user
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            FullName = request.FullName,
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = passwordHash,
            IsEmailConfirmed = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        // Create the company
        var company = new Company
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.CompanyName,
            Description = string.Empty,
            AddressLine1 = request.Address.Address,
            AddressLine2 = request.Address.Address2,
            City = request.Address.City,
            State = request.Address.State,
            Country = request.Address.Country,
            ZipCode = request.Address.ZipCode,
            OwnerUserId = user.Id,
            PlanId = plan.Id
        };

        // Create the company user relationship
        var userCompany = new CompanyUser
        {
            Id = Guid.NewGuid().ToString(),
            CompanyId = company.Id,
            UserId = user.Id
        };

        // Create the subscription
        var subscription = new CompanySubscription
        {
            Id = Guid.NewGuid().ToString(),
            CompanyId = company.Id,
            PlanId = plan.Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(plan.DurationInMonths),
            IsTrial = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        // Start the transaction
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                // Add the user, company, subscription, and permissions to the database
                dbContext.Users.Add(user);
                dbContext.Companies.Add(company);
                dbContext.CompanySubscriptions.Add(subscription);
                dbContext.CompanyUsers.Add(userCompany);
                dbContext.UserPermissions.AddRange(permissions.Select(permission => new UserPermission
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = user.Id,
                    ModulePermissionId = permission.Id,
                    CreatedAt = DateTime.UtcNow
                }));

                // Save changes and commit transaction
                await dbContext.SaveChangesAsync();
                transaction.Commit();

                // Return success
                return Results.Ok(Outcome<string>.Success(user.Id.Value));
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
