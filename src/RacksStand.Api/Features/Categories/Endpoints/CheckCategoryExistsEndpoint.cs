using RacksStand.Api.Core;
using RacksStand.Api.Features.Categories.Services;

namespace RacksStand.Api.Features.Categories.Endpoints;

public class CheckCategoryExistsEndpoint : EndpointBase
{
    public override string RouteName => "checkCategoryExists";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("shops/{shopId}/categories/exists", CheckCategoryExists)
            .WithName(RouteName)
            .Produces<bool>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Check if a category exists in the shop.",
                Description = "Checks if a category with the specified name exists within the shop.",
                Tags = new[] { new OpenApiTag { Name = "Categories" } },
                Parameters = new List<OpenApiParameter>
                {
                    new OpenApiParameter
                    {
                        Name = "categoryName",
                        In = ParameterLocation.Query,
                        Required = true,
                        Description = "The name of the category to check for existence.",
                        Schema = new OpenApiSchema { Type = "string" }
                    }
                },
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Category existence status returned." },
                    ["400"] = new OpenApiResponse { Description = "Invalid request." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> CheckCategoryExists
    (
        [FromRoute] string shopId,
        IValidator<string> idValidator,
        [FromQuery] string categoryName,
        ICategoryService categoryService,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await idValidator.ValidateAsync(shopId, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        if (string.IsNullOrEmpty(categoryName))
        {
            return Results.BadRequest("Category name must be provided.");
        }

        var exists = await categoryService.CheckIfCategoryExistsAsync(categoryName, shopId, cancellationToken);

        return Results.Ok(exists);
    }
}

