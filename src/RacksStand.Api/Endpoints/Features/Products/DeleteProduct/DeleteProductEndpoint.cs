using RacksStand.Api.Endpoints.Core;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.Api.Endpoints.Features.Products.DeleteProduct;

public class DeleteProductEndpoint : EndpointBase
{
    public override string RouteName => "deleteProduct";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id}", DeleteProduct)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Delete a product.",
                Description = "Marks a product as inactive in the system.",
                Tags = new[] { new OpenApiTag { Name = "Products" } }
            });
    }

    private async Task<IResult> DeleteProduct(
        [FromRoute] string id,
        IProductService productService,
        CancellationToken cancellationToken = default)
    {
        var response = await productService.DeleteProductAsync(id, cancellationToken);
        return response.Match(
            data => Results.Ok(new { Message = "Product deleted successfully.", ProductId = data }),
            error => Problem(error)
        );
    }
}

