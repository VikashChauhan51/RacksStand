using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Core.Infrastructure.Services;
using RacksStand.Api.Features.Categories.Endpoints.Requests;

namespace RacksStand.Api.Features.Categories.Endpoints;

public class GetCategoriesEndpoint : EndpointBase
{
    public override string RouteName => "getCategories";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("shops/{shopId}/categories", ListCategories)
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
        IValidator<GetCategoriesRequest> validator,
         ICategoryService categoryService,
        CancellationToken cancellationToken = default
        )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }
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
