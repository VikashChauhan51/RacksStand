using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Features.Suppliers.Models;
using RacksStand.Api.Features.Suppliers.Services;
using RacksStand.Models;

namespace RacksStand.Api.Features.Suppliers.Endpoints;

public class CreateSupplierEndpoint : EndpointBase
{
    public override string RouteName => "createSupplier";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("shops/{shopId}/suppliers", CreateSupplier)
            .WithName(RouteName)
            .Accepts<SupplierRequest>(MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Creates a new supplier.",
                Description = "Creates a new supplier with the provided name.",
                Tags = [new() { Name = "Suppliers" }],
                Responses = new OpenApiResponses
                {
                    ["201"] = new OpenApiResponse { Description = "Supplier created successfully." },
                    ["400"] = new OpenApiResponse { Description = "Bad Request. Validation errors." },
                    ["401"] = new OpenApiResponse { Description = "Unauthorized. Authentication is required." },
                    ["403"] = new OpenApiResponse { Description = "Forbidden. You do not have permission to access this resource." },
                    ["429"] = new OpenApiResponse { Description = "Too Many Requests. Rate limit exceeded." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> CreateSupplier
    (
        [FromRoute] string shopId,
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

        var response = await supplierService.AddSupplierAsync
        (
            request.Adapt<SupplierModel>() with
            {
                ShopId = shopId
            },
            cancellationToken
        );
        return response.Match
        (
            _ => Results.Created(),
            error => Problem(error)
        );
    }
}
