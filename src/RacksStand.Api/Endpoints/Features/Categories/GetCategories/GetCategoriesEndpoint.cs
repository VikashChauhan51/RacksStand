using RacksStand.Api.Endpoints.Core;

namespace RacksStand.Api.Endpoints.Features.Categories.GetCategories;

public class GetCategoriesEndpoint : EndpointBase
{
    public override string RouteName => "getCategories";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", ListCategories)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Lists all categories.",
                Description = "Fetches a list of all categories with optional filters.",
                Tags = [new() { Name = "Categories" }],
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Categories retrieved successfully." },
                    ["429"] = new OpenApiResponse { Description = "Too Many Requests. Rate limit exceeded." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> ListCategories(
        [AsParameters] GetCategoriesRequest request
        )
    {
        //var response = await mediator.Send(request.Adapt<GetCategoriesQuery>());
        //return response.Match
        //(
        //    data => Results.Ok(data.Categories),
        //    error => Problem(error)
        //);

        return Results.Ok();
    }
}
