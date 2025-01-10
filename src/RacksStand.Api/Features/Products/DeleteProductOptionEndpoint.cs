using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Products;

public class DeleteProductOptionEndpoint : EndpointBase
{
    public DeleteProductOptionEndpoint()
    {
        RequireAuthorization();
    }

    public override string RouteName => "deleteProductOption";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/product/{productId}/option/{optionId}", DeleteProductOptionAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
    }

    private static async Task<IResult> DeleteProductOptionAsync(
        [FromRoute] string productId,
        [FromRoute] string optionId,
        ILogger<DeleteProductOptionEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext)
    {
        var userName = httpContext.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            return Results.Unauthorized();
        }

        var productOption = await dbContext.ProductOptions
            .FirstOrDefaultAsync(o => o.Id == optionId && o.ProductId == productId);

        if (productOption == null)
        {
            logger.LogWarning("Product option not found with ID {OptionId} for product {ProductId}", optionId, productId);
            return Results.NotFound();
        }

        dbContext.ProductOptions.Remove(productOption);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}
