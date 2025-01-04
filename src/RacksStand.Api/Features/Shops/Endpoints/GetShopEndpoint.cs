using RacksStand.Models;
using RacksStand.Api.Features.Shops.Services;
using RacksStand.Api.Core;

namespace RacksStand.Api.Features.Shops.Endpoints;

public class GetShopEndpoint : EndpointBase
{
    public override string RouteName => "getShop";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("shops/{id}", GetShopById)
            .WithName(RouteName)
            .Produces<ShopModel>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Get shop details by ID.",
                Description = "Fetches the details of a specific shop based on its ID.",
                Tags = new[] { new OpenApiTag { Name = "Shops" } },
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Shop retrieved successfully." },
                    ["404"] = new OpenApiResponse { Description = "Shop not found." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> GetShopById(
        [FromRoute] string id,
        IShopService shopService,
        CancellationToken cancellationToken = default
    )
    {
        var response = await shopService.GetShopAsync(id, cancellationToken);
        return response.Match(
            data => Results.Ok(data),
            error => Problem(error)
        );
    }
}
