using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Products;

public class UpdateProductVariantEndpoint : EndpointBase
{
    public UpdateProductVariantEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "updateProductVariant";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut<IResult>("/product/{productId}/variant/{variantId}", UpdateProductVariantAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
    }

    private static async Task<IResult> UpdateProductVariantAsync(
        [FromRoute] string productId,
        [FromRoute] string variantId,
        UpdateProductVariantRequest request,
        IValidator<UpdateProductVariantRequest> validator,
        ILogger<UpdateProductVariantEndpoint> logger,
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

        var existingVariant = await dbContext.ProductVariants
            .FirstOrDefaultAsync(v => v.Id == variantId && v.ProductId == productId);

        if (existingVariant == null)
        {
            logger.LogWarning("The Variant not found for product: {ProductId} and variant:{variantId} ", productId, variantId);
            return Results.NotFound();
        }

        existingVariant.Name = request.Name;
        existingVariant.Description = request.Description;
        existingVariant.Code = request.Code;
        existingVariant.Sku = request.Sku;
        existingVariant.Barcode = request.Barcode != null ? Barcode.Of(request.Barcode) : null;
        existingVariant.BarcodeFormat = request.BarcodeFormat != null ? Enum.Parse<BarcodeFormat>(request.BarcodeFormat, true) : null;
        existingVariant.PurchasePrice = request.PurchasePrice;
        existingVariant.SellingPrice = request.SellingPrice;
        existingVariant.CurrencyCode = Enum.Parse<CurrencyCode>(request.CurrencyCode);
        existingVariant.Height = request.Height;
        existingVariant.Width = request.Width;
        existingVariant.Length = request.Length;
        existingVariant.DiminsionUnit = Enum.Parse<DimensionUnit>(request.DiminsionUnit);
        existingVariant.Weight = request.Weight;
        existingVariant.WeightUnit = Enum.Parse<WeightUnit>(request.WeightUnit);
        existingVariant.SupplierId = request.SupplierId != null ? SupplierId.Of(request.SupplierId) : null;
        existingVariant.ImageId = request.AttachmentId != null ? AttachmentId.Of(request.AttachmentId) : null;

        await dbContext.SaveChangesAsync();
        return Results.NoContent();
    }
}
