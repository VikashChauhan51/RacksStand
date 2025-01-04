using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Infrastructure.Data.Contexts;
using RacksStand.Infrastructure.Services;
using ResultifyCore.Outcome;

namespace RacksStand.Api.Features.Users.Signup;


public class SignupMutation
{
    private readonly IValidator<SignupRequest> validator;
    private readonly ILogger<SignupMutation> logger;
    public SignupMutation
    (
        IValidator<SignupRequest> validator,
        ILogger<SignupMutation> logger
    )
    {
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Outcome<string>> SignUpAsync(SignupRequest request, [Service] RacksStandContext dbContext)
    {
        logger.LogInformation("Creating an new account");
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Outcome<string>.Failure
            (
                validationResult.Errors.Select
                (
                    x => new OutcomeError(x.ErrorCode, x.ErrorMessage)
                )
            );
        }

        var isUserExists = await dbContext.Users.Where(x => x.Email.ToLower() == request.Email.ToLower()).AnyAsync();
        if (isUserExists)
        {
            logger.LogWarning("The user already exists for email {Email}", request.Email);
            return Outcome<string>.Failure
           (
                new OutcomeError("UserExists", "The User already exists.")
           );
        }

        var plan = await dbContext.Plans.Where(x => x.DurationInMonths == 1).FirstAsync();
        var permissions = await dbContext.Permissions.ToListAsync();
        var passwordHash = PasswordHasher.HashPassword(request.Password);
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

        var company = new Company
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.CompanyName,
            Description = string.Empty,
            AddressLine1 = request.Address,
            AddressLine2 = request.Address2,
            City = request.City,
            State = request.State,
            Country = request.Country,
            ZipCode = request.ZipCode,
            OwnerUserId = user.Id,
            PlanId = plan!.Id

        };

        var userCompany = new CompanyUser
        {
            Id = Guid.NewGuid().ToString(),
            CompanyId = company.Id,
            UserId = user.Id
        };

        var subscription = new CompanySubscription
        {
            Id = Guid.NewGuid().ToString(),
            CompanyId = company.Id,
            PlanId = plan!.Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(plan.DurationInMonths),
            IsTrial = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow

        };

        var transaction = dbContext.Database.BeginTransaction();

        try
        {
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
            await dbContext.SaveChangesAsync();
            transaction.Commit();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occured");
            transaction.Rollback();
            throw;
        }

        return Outcome<string>.Success(user.Id.Value);
    }
}
