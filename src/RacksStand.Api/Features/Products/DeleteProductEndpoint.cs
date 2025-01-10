using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Products;

public class DeleteProductEndpoint : EndpointBase
{
    public DeleteProductEndpoint()
    {
        RequireAuthorization();
    }

    public override string RouteName => "deleteProduct";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{categoryId}/product/{productId}", DeleteProductAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
    }

    private static async Task<IResult> DeleteProductAsync(
        [FromRoute] string categoryId,
        [FromRoute] string productId,
        ILogger<DeleteProductEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext)
    {
        var userName = httpContext.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            return Results.Unauthorized();
        }

        var product = await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == productId && p.CategoryId == categoryId);

        if (product == null)
        {
            logger.LogWarning("Product not found with ID {ProductId} in category {CategoryId}", productId, categoryId);
            return Results.NotFound();
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}
