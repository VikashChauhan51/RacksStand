using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Core.Infrastructure.Services;
using RacksStand.Api.Features.Suppliers.Endpoints.Requests;

namespace RacksStand.Api.Features.Suppliers.Endpoints;

public class GetSuppliersEndpoint : EndpointBase
{
    public override string RouteName => "getSuppliers";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("shops/{shopId}/suppliers", ListSuppliers)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Lists all suppliers.",
                Description = "Fetches a list of all suppliers with optional filters.",
                Tags = [new() { Name = "Suppliers" }],
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Suppliers retrieved successfully." },
                    ["429"] = new OpenApiResponse { Description = "Too Many Requests. Rate limit exceeded." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> ListSuppliers(
        [FromRoute] string shopId,
        [AsParameters] GetSuppliersRequest request,
         ISupplierService supplierService,
        CancellationToken cancellationToken = default
        )
    {
        var response = await supplierService.GetSuppliersAsync
        (
            shopId,
            request.pageSize,
            request.pageNumber,
            request.filter,
            cancellationToken
        );
        return response.Match
        (
            data => Results.Ok(data),
            error => Problem(error)
        );
    }
}
