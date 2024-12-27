using RacksStand.Api.Endpoints.Core;
using RacksStand.UnifiedServices.DataServices;
using RacksStand.Models;

namespace RacksStand.Api.Endpoints.Features.Shops.UpdateShop;

public class UpdateShopEndpoint : EndpointBase
{
    public override string RouteName => "updateShop";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("shops/{id}", UpdateShop)
            .WithName(RouteName)
            .Produces<string>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Updates shop details.",
                Description = "Updates the details of a specific shop.",
                Tags = new[] { new OpenApiTag { Name = "Shops" } },
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Shop updated successfully." },
                    ["400"] = new OpenApiResponse { Description = "Invalid request." },
                    ["404"] = new OpenApiResponse { Description = "Shop not found." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> UpdateShop(
        [FromRoute] string id,
        [FromBody] UpdateShopRequest request,
        IValidator<UpdateShopRequest> validator,
        IShopService shopService,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await shopService.UpdateShopAsync
        (
            id,
            request.Adapt<ShopModel>(),
            cancellationToken
        );
        return response.Match(
            data => Results.Ok(data),
            error => Problem(error)
        );
    }
}
