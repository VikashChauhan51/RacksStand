using RacksStand.Api.Endpoints.Core;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.Api.Endpoints.Features.Products.CheckProductExists;

public class CheckProductExistsEndpoint : EndpointBase
{
    public override string RouteName => "checkProductExists";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/check", CheckProductExists)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Check if a product exists.",
                Description = "Checks if a product with a given name and category exists.",
                Tags = new[] { new OpenApiTag { Name = "Products" } }
            });
    }

    private async Task<IResult> CheckProductExists(
        [FromQuery] string name,
        [FromQuery] string categoryId,
        IProductService productService,
        CancellationToken cancellationToken = default)
    {
        var response = await productService.CheckIfProductExistsAsync(name, categoryId, cancellationToken);
        return response.Match(
            exists => Results.Ok(new { Exists = exists }),
            error => Problem(error)
        );
    }
}

