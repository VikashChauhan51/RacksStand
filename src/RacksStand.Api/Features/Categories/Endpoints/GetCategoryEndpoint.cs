using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Features.Categories.Domain.Services;

namespace RacksStand.Api.Features.Categories.Endpoints;

public class GetCategoryEndpoint : EndpointBase
{
    public override string RouteName => "getCategory";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("shops/{shopId}/categories/{id}", GetCategory)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Gets a category by ID.",
                Description = "Fetches the details of a specific category by its unique identifier.",
                Tags = [new() { Name = "Categories" }],
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Category retrieved successfully." },
                    ["404"] = new OpenApiResponse { Description = "Category not found." },
                    ["429"] = new OpenApiResponse { Description = "Too Many Requests. Rate limit exceeded." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> GetCategory
    (
        [FromRoute] string id,
        IValidator<string> idValidator,
        ICategoryService categoryService,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await idValidator.ValidateAsync(id, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await categoryService.GetCategoryAsync(id, cancellationToken);
        return response.Match
        (
            data => Results.Ok(data),
            error => Problem(error)
        );
    }
}

