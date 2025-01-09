using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Categories;

public class UpdateCategoryEndpoint : EndpointBase
{
    public UpdateCategoryEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "updateCategory";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut<IResult>("/category/{categoryId}", CreateCategoryAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<NotFound>(StatusCodes.Status404NotFound)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Update category endpoint",
                Description = "Get a user's all assigned copanies.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> CreateCategoryAsync(
        [FromRoute] string categoryId,
        CategoryRequest request,
        IValidator<CategoryRequest> validator,
        ILogger<UpdateCategoryEndpoint> logger,
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

        if (string.IsNullOrEmpty(categoryId))
        {
            return Results.BadRequest();
        }
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var category = await dbContext.Categories.FindAsync(categoryId);
        if (category == null)
        {
            logger.LogError("The category not found for id:{CategoryId}", categoryId);
            return Results.NotFound();
        }

        category.Name = request.Name;
        category.Description = request.Description;

        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}
