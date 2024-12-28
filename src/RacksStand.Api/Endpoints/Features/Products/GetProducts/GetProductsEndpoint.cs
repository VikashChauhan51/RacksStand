using RacksStand.Api.Endpoints.Core;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.Api.Endpoints.Features.Products.GetProducts;

public class GetProductsEndpoint : EndpointBase
{
    public override string RouteName => "getProducts";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products", GetProducts)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Get all products.",
                Description = "Fetches a list of all products.",
                Tags = new[] { new OpenApiTag { Name = "Products" } }
            });
    }

    private async Task<IResult> GetProducts(
        IProductService productService,
        CancellationToken cancellationToken = default)
    {
        // Replace with your actual method to fetch all products.
        var response = await productService.GetAllProductsAsync(cancellationToken);
        return Results.Ok(response);
    }
}

