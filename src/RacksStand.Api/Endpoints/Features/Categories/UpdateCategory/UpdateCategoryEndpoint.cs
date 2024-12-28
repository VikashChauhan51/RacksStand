using RacksStand.Api.Endpoints.Core;
using RacksStand.UnifiedServices.DataServices;
using RacksStand.Models;

namespace RacksStand.Api.Endpoints.Features.Categories.UpdateCategory;

public class UpdateCategoryEndpoint : EndpointBase
{
    public override string RouteName => "updateCategory";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("{shopId}/categories/{id}", UpdateCategory)
            .WithName(RouteName)
            .Accepts<UpdateCategoryRequest>(MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Updates an existing category.",
                Description = "Updates the details of an existing category using the provided information.",
                Tags = [new() { Name = "Categories" }],
                Responses = new OpenApiResponses
                {
                    ["200"] = new OpenApiResponse { Description = "Category updated successfully." },
                    ["400"] = new OpenApiResponse { Description = "Bad Request. Validation errors." },
                    ["401"] = new OpenApiResponse { Description = "Unauthorized. Authentication is required." },
                    ["403"] = new OpenApiResponse { Description = "Forbidden. You do not have permission to access this resource." },
                    ["404"] = new OpenApiResponse { Description = "Not Found. The category does not exist." },
                    ["429"] = new OpenApiResponse { Description = "Too Many Requests. Rate limit exceeded." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> UpdateCategory
    (
       [FromRoute] string id,
        UpdateCategoryRequest request,
        IValidator<UpdateCategoryRequest> validator,
        ICategoryService categoryService,
        CancellationToken cancellationToken = default
    )
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await categoryService.UpdateCategoryAsync(id, request.Adapt<CategoryModel>(), cancellationToken);
        return response.Match
        (
            _ => Results.Ok(),
            error => Problem(error)
        );
    }
}

