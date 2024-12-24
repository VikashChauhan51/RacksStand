using RacksStand.Api.Endpoints.Core;

namespace RacksStand.Api.Endpoints.Features.Products.GetProducts;

public class GetProductsEndpoint : EndpointBase
{
    public override string RouteName => "getProducts";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products", GetProducts)
            .WithOpenApi();
    }

    private async Task<IResult> GetProducts(HttpRequest req, ILogger<GetProductsEndpoint> logger)
    {
        var version = int.Parse((string)req.RouteValues["apiVersion"]!);

        logger.LogInformation("Getting products");
        if (version == 1)
        {
           
            return Results.Ok(new { message = "Products V1" }); // Return V1 data
        }
        else if (version == 2)
        {
            return Results.Ok(new { message = "Products V2" }); // Return V2 data
        }
        else
        {
            return Results.BadRequest(new { message = "Invalid API version" });
        }
    }

}
