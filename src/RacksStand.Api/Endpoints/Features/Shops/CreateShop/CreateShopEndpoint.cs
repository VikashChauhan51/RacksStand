using RacksStand.Api.Endpoints.Core;
using RacksStand.Api.Endpoints.Features.Shops.Shared;
using RacksStand.Persistence.Entities;
using RacksStand.Models;

namespace RacksStand.Api.Endpoints.Features.Shops.CreateShop;

public class CreateShopEndpoint : EndpointBase
{
    public override string RouteName => "createShop";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("shops", CreateShop)
            .WithName(RouteName)
            .Accepts<CreateShopRequest>(MediaTypeNames.Application.Json)
            .Produces<Shop>(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Creates a new shop.",
                Description = "Creates a new shop with the provided name, address, and owner information.",
                Tags = new[] { new OpenApiTag { Name = "Shops" } },
                Responses = new OpenApiResponses
                {
                    ["201"] = new OpenApiResponse { Description = "Shop created successfully." },
                    ["400"] = new OpenApiResponse { Description = "Bad Request. Validation errors." },
                    ["401"] = new OpenApiResponse { Description = "Unauthorized. Authentication is required." },
                    ["403"] = new OpenApiResponse { Description = "Forbidden. You do not have permission to access this resource." },
                    ["429"] = new OpenApiResponse { Description = "Too Many Requests. Rate limit exceeded." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> CreateShop
    (
        CreateShopRequest request,
        IValidator<CreateShopRequest> validator,
        IShopService shopService,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await shopService.AddShopAsync(
            request.Adapt<ShopModel>() with { OwnerId = "123" }, //TODO:replace with userId
            cancellationToken
        );

        return response.Match
       (
           _ => Results.Created(),
           error => Problem(error)
       );
    }
}
