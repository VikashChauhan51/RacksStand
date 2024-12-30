using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Features.Suppliers.Services;

namespace RacksStand.Api.Features.Suppliers.Endpoints;

public class CheckSupplierExistsEndpoint : EndpointBase
{
    public override string RouteName => "checkSupplierExists";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("shops/{shopId}/suppliers/exists", CheckSupplierExists)
            .WithName(RouteName)
            .Produces<bool>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Check if a Supplier exists in the shop.",
                Description = "Checks if a Supplier with the specified name exists within the shop.",
                Tags = new[] { new OpenApiTag { Name = "Suppliers" } },
                Parameters = new List<OpenApiParameter>
                {
                    new OpenApiParameter
                    {
                        Name = "supplierName",
                        In = ParameterLocation.Query,
                        Required = true,
                        Description = "The name of the Supplier to check for existence.",
                        Schema = new OpenApiSchema { Type = "string" }
                    }
                },
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Supplier existence status returned." },
                    ["400"] = new OpenApiResponse { Description = "Invalid request." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> CheckSupplierExists
    (
        [FromRoute] string shopId,
        [FromQuery] string supplierName,
        ISupplierService supplierService,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrEmpty(supplierName))
        {
            return Results.BadRequest("Supplier name must be provided.");
        }
        var exists = await supplierService.CheckIfSupplierExistsAsync(supplierName, shopId, cancellationToken);

        return Results.Ok(exists);
    }
}

