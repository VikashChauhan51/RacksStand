using RacksStand.Api.Endpoints.Core;
using RacksStand.Api.Endpoints.Features.Categories.Shared;

namespace RacksStand.Api.Endpoints.Features.Categories.GetCategories;

public class GetCategoriesEndpoint : EndpointBase
{
    public override string RouteName => "getCategories";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("{shopId}/categories", ListCategories)
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
        [FromRoute] string shopId,
        [AsParameters] GetCategoriesRequest request,
         ICategoryService categoryService,
        CancellationToken cancellationToken = default
        )
    {
        var response = await categoryService.GetCategoriesAsync
        (
            shopId,
            request.pageSize,
            request.pageNumber,
            request.filter,
            cancellationToken
        );
        return response.Match
        (
            data => Results.Ok(data),
            error => Problem(error)
        );
    }
}
