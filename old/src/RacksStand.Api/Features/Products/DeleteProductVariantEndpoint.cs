using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Products;

public class DeleteProductVariantEndpoint : EndpointBase
{
    public DeleteProductVariantEndpoint()
    {
        RequireAuthorization();
    }

    public override string RouteName => "deleteProductVariant";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/product/{productId}/variant/{variantId}", DeleteProductVariantAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
    }

    private static async Task<IResult> DeleteProductVariantAsync(
        [FromRoute] string productId,
        [FromRoute] string variantId,
        ILogger<DeleteProductVariantEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext)
    {
        var userName = httpContext.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            return Results.Unauthorized();
        }

        var productVariant = await dbContext.ProductVariants
            .FirstOrDefaultAsync(v => v.Id == variantId && v.ProductId == productId);

        if (productVariant == null)
        {
            logger.LogWarning("Product variant not found with ID {VariantId} for product {ProductId}", variantId, productId);
            return Results.NotFound();
        }

        dbContext.ProductVariants.Remove(productVariant);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}
