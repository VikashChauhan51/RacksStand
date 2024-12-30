using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Core.Infrastructure.Services;

namespace RacksStand.Api.Features.Shops.Endpoints;

public class DeleteShopEndpoint : EndpointBase
{
    public override string RouteName => "deleteShop";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("shops/{id}", DeleteShopById)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Delete a shop by ID.",
                Description = "Deletes a shop by its unique identifier.",
                Tags = new[] { new OpenApiTag { Name = "Shops" } },
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Shop deleted successfully." },
                    ["404"] = new OpenApiResponse { Description = "Shop not found." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> DeleteShopById(
        [FromRoute] string id,
        IShopService shopService,
        CancellationToken cancellationToken = default
    )
    {
        var response = await shopService.DeleteShopAsync(id, cancellationToken);
        return response.Match(
            data => Results.Ok(new { Message = "Shop deleted successfully.", ShopId = data }),
            error => Problem(error)
        );
    }
}

