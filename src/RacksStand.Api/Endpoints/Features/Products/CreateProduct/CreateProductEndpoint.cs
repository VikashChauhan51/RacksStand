using RacksStand.Api.Endpoints.Core;
using RacksStand.Api.Endpoints.Features.Products.Shared;
using RacksStand.Models;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.Api.Endpoints.Features.Products.CreateProduct;

public class CreateProductEndpoint : EndpointBase
{
    public override string RouteName => "createProduct";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/categories/{categoryId}/products", CreateProduct)
            .WithName(RouteName)
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Create a new product.",
                Description = "Adds a new product to the system.",
                Tags = new[] { new OpenApiTag { Name = "Products" } }
            });
    }

    private async Task<IResult> CreateProduct(
        [FromRoute] string categoryId,
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
        validationResult = await idValidator.ValidateAsync(categoryId, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await productService.AddProductAsync(
        request.Adapt<ProductModel>() with
        {
            CategoryId = categoryId
        },
        cancellationToken);
        return response.Match(
            id => Results.Created(),
            error => Problem(error)
        );
    }
}

