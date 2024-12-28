using RacksStand.Api.Endpoints.Core;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.Api.Endpoints.Features.Products.GetProduct;

public class GetProductEndpoint : EndpointBase
{
    public override string RouteName => "getProduct";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("categories/{categoryId}/products/{id}", GetProduct)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Get product details.",
                Description = "Fetches details of a product by its ID.",
                Tags = new[] { new OpenApiTag { Name = "Products" } }
            });
    }

    private async Task<IResult> GetProduct(
        [FromRoute] string id,
        IValidator<string> idValidator,
        IProductService productService,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await idValidator.ValidateAsync(id, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await productService.GetProductAsync(id, cancellationToken);
        return response.Match(
            data => Results.Ok(data),
            error => Problem(error)
        );
    }
}

