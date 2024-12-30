using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Core.Infrastructure.Services;

namespace RacksStand.Api.Features.Suppliers.Endpoints;

public class GetSupplierEndpoint : EndpointBase
{
    public override string RouteName => "getSupplier";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("shops/{shopId}/suppliers/{id}", GetSupplier)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Gets a supplier by ID.",
                Description = "Fetches the details of a specific supplier by its unique identifier.",
                Tags = [new() { Name = "Suppliers" }],
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Supplier retrieved successfully." },
                    ["404"] = new OpenApiResponse { Description = "Supplier not found." },
                    ["429"] = new OpenApiResponse { Description = "Too Many Requests. Rate limit exceeded." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> GetSupplier
    (
        [FromRoute] string id,
        ISupplierService supplierService,
        CancellationToken cancellationToken = default
    )
    {
        var response = await supplierService.GetSupplierAsync(id, cancellationToken);
        return response.Match
        (
            data => Results.Ok(data),
            error => Problem(error)
        );
    }
}

