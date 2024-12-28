using RacksStand.Api.Endpoints.Core;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.Api.Endpoints.Features.Categories.DeleteCategory;

public class DeleteCategoryEndpoint : EndpointBase
{
    public override string RouteName => "deleteCategory";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("{shopId}/categories/{id}", DeleteCategory)
            .WithName(RouteName)
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Deletes a category by ID.",
                Description = "Marks the category as inactive or deletes it by its unique identifier.",
                Tags = [new() { Name = "Categories" }],
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Category deleted successfully." },
                    ["404"] = new OpenApiResponse { Description = "Category not found." },
                    ["429"] = new OpenApiResponse { Description = "Too Many Requests. Rate limit exceeded." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> DeleteCategory
    (
        [FromRoute] string id,
        ICategoryService categoryService,
        CancellationToken cancellationToken = default
    )
    {
        var response = await categoryService.DeleteCategoryAsync(id, cancellationToken);
        return response.Match
        (
            data => Results.Ok(new { Message = "Category deleted successfully.", CategoryId = data }),
            error => Problem(error)
        );
    }
}

