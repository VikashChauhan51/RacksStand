using RacksStand.Api.Endpoints.Core;
using RacksStand.Models;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.Api.Endpoints.Features.Products.CreateProduct;

public class CreateProductEndpoint : EndpointBase
{
    public override string RouteName => "createProduct";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("products", CreateProduct)
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
        [FromBody] ProductModel model,
        IProductService productService,
        CancellationToken cancellationToken = default)
    {
        var response = await productService.AddProductAsync(model, cancellationToken);
        return response.Match(
            id => Results.Created($"products/{id}", new { ProductId = id }),
            error => Problem(error)
        );
    }
}

