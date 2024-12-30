using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Features.Suppliers.Services;

namespace RacksStand.Api.Features.Suppliers.Endpoints;

public class DeleteSupplierEndpoint : EndpointBase
{
    public override string RouteName => "deleteSupplier";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("shops/{shopId}/suppliers/{id}", DeleteSupplier)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Delete a supplier by ID.",
                Description = "Deletes a specific supplier by its unique identifier.",
                Tags = new[] { new OpenApiTag { Name = "Suppliers" } },
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Supplier deleted successfully." },
                    ["404"] = new OpenApiResponse { Description = "Supplier not found." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> DeleteSupplier(
        [FromRoute] string id,
        ISupplierService supplierService,
        CancellationToken cancellationToken = default
    )
    {
        var response = await supplierService.DeleteSupplierAsync(id, cancellationToken);
        return response.Match(
            data => Results.Ok(new { Message = "Supplier deleted successfully.", SupplierId = data }),
            error => Problem(error)
        );
    }
}

