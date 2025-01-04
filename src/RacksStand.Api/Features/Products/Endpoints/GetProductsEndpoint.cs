using RacksStand.Api.Core;
using RacksStand.Api.Features.Products.Models;
using RacksStand.Api.Features.Products.Services;

namespace RacksStand.Api.Features.Products.Endpoints;

public class GetProductsEndpoint : EndpointBase
{
    public override string RouteName => "getProducts";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("categories/{categoryId}/products", GetProducts)
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
         [FromRoute] string categoryId,
        [AsParameters] GetProductsRequest request,
        IValidator<GetProductsRequest> validator,
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

        var response = await productService.GetProductsAsync
        (
            categoryId,
            request.pageSize,
            request.pageNumber,
            request.filter,
            cancellationToken
        );
        return Results.Ok(response);
    }
}

