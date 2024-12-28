using RacksStand.Api.Endpoints.Core;
using RacksStand.Models;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.Api.Endpoints.Features.Products.UpdateProduct;

public class UpdateProductEndpoint : EndpointBase
{
    public override string RouteName => "updateProduct";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id}", UpdateProduct)
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
        [FromRoute] string id,
        [FromBody] ProductModel model,
        IProductService productService,
        CancellationToken cancellationToken = default)
    {
        var response = await productService.UpdateProductAsync(id, model, cancellationToken);
        return response.Match(
            data => Results.Ok(new { Message = "Product updated successfully.", ProductId = data }),
            error => Problem(error)
        );
    }
}

