using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Products;

public class UpdateProductVariantOptionValueEndpoint : EndpointBase
{
    public UpdateProductVariantOptionValueEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "updateProductVariantOptionValue";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut<IResult>("/variant/{variantId}/option/{optionId}/value/{valueId}", UpdateProductVariantOptionValueAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
    }

    private static async Task<IResult> UpdateProductVariantOptionValueAsync(
        [FromRoute] string variantId,
        [FromRoute] string optionId,
        [FromRoute] string valueId,
        ProductVariantOptionRequest request,
        IValidator<ProductVariantOptionRequest> validator,
        ILogger<UpdateProductVariantOptionValueEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext)
    {
        var userName = httpContext.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            return Results.Unauthorized();
        }

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var existingValue = await dbContext.ProductVariantOptions
            .FirstOrDefaultAsync(v => v.Id == valueId && v.ProductVariantId == variantId && v.ProductOptionId == optionId);

        if (existingValue == null)
        {
            logger.LogWarning("The Variant Option not found for Variant:{VariantId}", variantId);
            return Results.NotFound();
        }
        existingValue.Value = request.Value;

        await dbContext.SaveChangesAsync();
        return Results.NoContent();
    }
}
