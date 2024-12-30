using RacksStand.Api.Core.Infrastructure;
using RacksStand.Models;
using RacksStand.Api.Core.Infrastructure.Services;
using RacksStand.Api.Features.Products.Models;

namespace RacksStand.Api.Features.Products.Endpoints;

public class UpdateProductEndpoint : EndpointBase
{
    public override string RouteName => "updateProduct";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("categories/{categoryId}/products/{id}", UpdateProduct)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Update a product.",
                Description = "Updates the details of an existing product.",
                Tags = new[] { new OpenApiTag { Name = "Products" } }
            });
    }

    private async Task<IResult> UpdateProduct(
        [FromRoute] string categoryId,
        [FromRoute] string id,
        [FromBody] ProductRequest request,
        IValidator<ProductRequest> validator,
        IValidator<string> idValidator,
        IProductService productService,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }
        validationResult = await idValidator.ValidateAsync(id, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        validationResult = await idValidator.ValidateAsync(categoryId, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await productService.UpdateProductAsync(id, request.Adapt<ProductModel>() with
        {
            CategoryId = categoryId
        }, cancellationToken);
        return response.Match(
            data => Results.Ok(new { Message = "Product updated successfully.", ProductId = data }),
            error => Problem(error)
        );
    }
}

