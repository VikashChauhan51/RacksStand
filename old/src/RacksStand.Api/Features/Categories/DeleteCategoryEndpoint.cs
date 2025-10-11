using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Categories;

public class DeleteCategoryEndpoint : EndpointBase
{
    public DeleteCategoryEndpoint()
    {
        RequireAuthorization();
    }

    public override string RouteName => "deleteCategory";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/category/{categoryId}", DeleteCategoryAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<NotFound>(StatusCodes.Status404NotFound)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Delete category endpoint",
                Description = "Delete a category by its ID.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();
    }

    private static async Task<IResult> DeleteCategoryAsync(
        [FromRoute] string categoryId,
        ILogger<DeleteCategoryEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext)
    {
        // Validate user authentication
        var userName = httpContext.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            return Results.Unauthorized();
        }

        var user = await dbContext.Users
            .AsNoTracking()
            .Where(x => x.UserName == userName)
            .Select(x => new { x.SelectedCompanyId, x.Id })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            logger.LogWarning("User not found for username claim: {UserName}", userName);
            return Results.Unauthorized();
        }

        // Validate categoryId
        if (string.IsNullOrEmpty(categoryId))
        {
            return Results.BadRequest();
        }

        // Find the category
        var category = await dbContext.Categories.FindAsync(categoryId);
        if (category == null)
        {
            logger.LogError("Category not found for id: {CategoryId}", categoryId);
            return Results.NotFound();
        }

        // Delete the category
        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Category with ID {CategoryId} deleted successfully", categoryId);
        return Results.NoContent();
    }
}
