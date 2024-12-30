using RacksStand.Models;
using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Features.Suppliers.Endpoints.Requests;
using RacksStand.Api.Core.Infrastructure.Services;

namespace RacksStand.Api.Features.Suppliers.Endpoints;

public class UpdateSupplierEndpoint : EndpointBase
{
    public override string RouteName => "updateSupplier";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("shops/{shopId}/suppliers/{id}", UpdateSupplier)
            .WithName(RouteName)
            .Accepts<SupplierRequest>(MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Updates an existing supplier.",
                Description = "Updates the details of an existing supplier using the provided information.",
                Tags = [new() { Name = "Suppliers" }],
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Supplier updated successfully." },
                    ["400"] = new OpenApiResponse { Description = "Bad Request. Validation errors." },
                    ["401"] = new OpenApiResponse { Description = "Unauthorized. Authentication is required." },
                    ["403"] = new OpenApiResponse { Description = "Forbidden. You do not have permission to access this resource." },
                    ["404"] = new OpenApiResponse { Description = "Not Found. The Supplier does not exist." },
                    ["429"] = new OpenApiResponse { Description = "Too Many Requests. Rate limit exceeded." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> UpdateSupplier
    (
       [FromRoute] string id,
        SupplierRequest request,
        IValidator<SupplierRequest> validator,
        ISupplierService supplierService,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await supplierService.UpdateSupplierAsync(id, request.Adapt<SupplierModel>(), cancellationToken);
        return response.Match
        (
            _ => Results.Ok(),
            error => Problem(error)
        );
    }
}

