using RacksStand.Api.Endpoints.Core;
using RacksStand.UnifiedServices.DataServices;
using RacksStand.Models;
using RacksStand.Api.Endpoints.Features.Shops.Shared;

namespace RacksStand.Api.Endpoints.Features.Shops.GetShops;

public class GetShopsEndpoint : EndpointBase
{
    public override string RouteName => "getShops";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("shops", ListShops)
            .WithName(RouteName)
            .Produces<PaginatedResult<ShopModel>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Lists all shops.",
                Description = "Fetches a list of all shops with optional filters.",
                Tags = new[] { new OpenApiTag { Name = "Shops" } },
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Shops retrieved successfully." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> ListShops(
        [AsParameters] GetShopsRequest request,
        IShopService shopService,
        IValidator<GetShopsRequest> validator,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }


        var ownerId = "55fddc82-40cf-4ba0-ad8e-b02dfcc8d9d0";
        var response = await shopService.GetShopsAsync(
            request.pageSize,
            request.pageNumber,
            ownerId,
            request.filter,
            cancellationToken
        );
        return response.Match(
            data => Results.Ok(data),
            error => Problem(error)
        );
    }
}
