using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Core.Infrastructure.Services;

namespace RacksStand.Api.Features.Shops.Endpoints;

public class ShopExistsEndpoint : EndpointBase
{
    public override string RouteName => "shopExists";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("shops/exists", ShopExists)
            .WithName(RouteName)
            .Produces<bool>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Checks if a shop exists.",
                Description = "Checks if a shop with the specified name and owner ID exists.",
                Tags = new[] { new OpenApiTag { Name = "Shops" } },
                Parameters = new List<OpenApiParameter>
                {
                    new OpenApiParameter
                    {
                        Name = "name",
                        In = ParameterLocation.Query,
                        Required = true,
                        Description = "The name of the shop.",
                        Schema = new OpenApiSchema { Type = "string" }
                    }
                },
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Returns whether the shop exists." },
                    ["400"] = new OpenApiResponse { Description = "Invalid request." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> ShopExists(
        [FromQuery] string name,
        IShopService shopService,
        CancellationToken cancellationToken = default
    )
    {
        var ownerId = "55fddc82-40cf-4ba0-ad8e-b02dfcc8d9d0";
        if (string.IsNullOrEmpty(name))
        {
            return Results.BadRequest("Both name and ownerId are required.");
        }
        var exists = await shopService.CheckIfShopExistsAsync(name, ownerId, cancellationToken);
        return Results.Ok(exists);
    }
}

