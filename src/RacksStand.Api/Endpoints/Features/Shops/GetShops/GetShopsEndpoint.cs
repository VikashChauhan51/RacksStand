using RacksStand.Api.Endpoints.Core;
using RacksStand.Api.Endpoints.Features.Shops.Shared;
using RacksStand.Models;

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
        [AsParameters] GetShopRequest request,
        IShopService shopService,
        CancellationToken cancellationToken = default
    )
    {
        var response = await shopService.GetShopsAsync(
            request.pageSize,
            request.pageNumber,
            request.filter,
            cancellationToken
        );
        return response.Match(
            data => Results.Ok(data),
            error => Problem(error)
        );
    }
}
