using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Domain.Entities;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Products;

public class CreateProductEndpoint : EndpointBase
{
    public CreateProductEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "createProduct";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost<IResult>("/product", CreateProductAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Create product endpoint",
                Description = "Get a user's all assigned copanies.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> CreateProductAsync(
        ProductRequest request,
        IValidator<ProductRequest> validator,
        ILogger<CreateProductEndpoint> logger,
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

        var product = new Product
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            CompanyId = user.SelectedCompanyId,
            CategoryId = request.CategoryId,
            Description = request.Description,
            Brand = request.Brand,
            Type = request.Type,
            Tags = request.Tags,
            Metadata = request.Metadata
        };

        var productOptions = new List<ProductOption>();
        if (request.ProductOptions?.Any() == true)
        {
            productOptions
                          .AddRange(request.ProductOptions
                          .Select(option =>
                              new ProductOption
                              {
                                  Id = Guid.NewGuid().ToString(),
                                  Name = option.Name,
                                  ProductId = product.Id
                              })
                          );
        }

        var productOptionsDict = productOptions.ToDictionary(o => o.Name, o => o.Id);

        var productVariants = new List<ProductVariant>();
        productVariants.AddRange
            (
                request.ProductVariants
                .Select(productVariant =>
                {

                    var variant = new ProductVariant
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProductId = product.Id,
                        Name = productVariant.Name,
                        Description = productVariant.Description,
                        Code = productVariant.Code,
                        Sku = productVariant.Sku,
                        Barcode = productVariant.Barcode != null ? Barcode.Of(productVariant.Barcode) : null,
                        BarcodeFormat = productVariant.BarcodeFormat != null ? Enum.Parse<BarcodeFormat>(productVariant.BarcodeFormat, true) : null,
                        PurchasePrice = productVariant.PurchasePrice,
                        SellingPrice = productVariant.SellingPrice,
                        CurrencyCode = Enum.Parse<CurrencyCode>(productVariant.CurrencyCode),
                        Height = productVariant.Height,
                        Width = productVariant.Width,
                        Length = productVariant.Length,
                        DiminsionUnit = Enum.Parse<DimensionUnit>(productVariant.DiminsionUnit),
                        Weight = productVariant.Weight,
                        WeightUnit = Enum.Parse<WeightUnit>(productVariant.WeightUnit),
                        SupplierId = productVariant.SupplierId != null ? SupplierId.Of(productVariant.SupplierId) : null,
                        ImageId = productVariant.AttachmentId != null ? AttachmentId.Of(productVariant.AttachmentId) : null

                    };

                    if (productOptions?.Any() == true)
                    {
                        foreach (var productVariantOptions in productVariant.ProductVariantOptions!)
                        {
                            variant.ProductVariantOptions.Add(new ProductVariantOption
                            {
                                Id = Guid.NewGuid().ToString(),
                                Value = productVariantOptions.Value,
                                ProductOptionId = productOptionsDict[productVariantOptions.Name],
                                ProductVariantId = variant.Id
                            });
                        }

                    }
                    return variant;
                }

                )
            );

        using (var transaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                dbContext.Products.Add(product);
                dbContext.ProductOptions.AddRange(productOptions);
                dbContext.ProductVariants.AddRange(productVariants);
                // Save changes and commit transaction
                await dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        return Results.Created();
    }
}
