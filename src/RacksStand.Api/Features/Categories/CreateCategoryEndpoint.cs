using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Domain.Entities;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Categories;

public class CreateCategoryEndpoint : EndpointBase
{
    public CreateCategoryEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "createCategory";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost<IResult>("/category", CreateCategoryAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Create category endpoint",
                Description = "Get a user's all assigned copanies.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> CreateCategoryAsync(
        CategoryRequest request,
        IValidator<CategoryRequest> validator,
        ILogger<CreateCategoryEndpoint> logger,
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
            .Select(x => new { x.SelectedCompanyId, x.Id })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            logger.LogWarning("User not found for username clam: {UserName}", userName);
            return Results.Unauthorized();
        }

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var category = new Category
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Description = request.Description,
            CompanyId = user.SelectedCompanyId
        };
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();

        return Results.Created();
    }
}
