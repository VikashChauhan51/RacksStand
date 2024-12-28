using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Features.Products.Services;

namespace RacksStand.Api.Features.Products.Endpoints.CheckProductExists;

public class CheckProductExistsEndpoint : EndpointBase
{
    public override string RouteName => "checkProductExists";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("categories/{categoryId}/products/exists", CheckProductExists)
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
        [FromRoute] string categoryId,
        IValidator<string> idValidator,
        IProductService productService,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await idValidator.ValidateAsync(categoryId, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await productService.CheckIfProductExistsAsync(name, categoryId, cancellationToken);
        return response.Match(
            exists => Results.Ok(exists),
            error => Problem(error)
        );
    }
}

