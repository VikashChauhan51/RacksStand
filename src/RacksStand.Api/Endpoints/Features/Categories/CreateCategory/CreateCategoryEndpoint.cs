using RacksStand.Api.Endpoints.Core;
using RacksStand.Application.Features.Categories.CreateCategory;
using System.Net.Mime;

namespace RacksStand.Api.Endpoints.Features.Categories.CreateCategory;

public class CreateCategoryEndpoint : EndpointBase
{
    public override string RouteName => "createCategory";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("categories", CreateCategory)
            .WithName(RouteName)
            .Accepts<CreateCategoryRequest>(MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status429TooManyRequests)
            .WithOpenApi(operation => new OpenApiOperation(operation)
            {
                Summary = "Creates a new category.",
                Description = "Creates a new category with the provided name.",
                Tags = [new() { Name = "Categories" }],
                Responses = new OpenApiResponses
                {
                    ["201"] = new OpenApiResponse { Description = "Category created successfully." },
                    ["400"] = new OpenApiResponse { Description = "Bad Request. Validation errors." },
                    ["401"] = new OpenApiResponse { Description = "Unauthorized. Authentication is required." },
                    ["403"] = new OpenApiResponse { Description = "Forbidden. You do not have permission to access this resource." },
                    ["429"] = new OpenApiResponse { Description = "Too Many Requests. Rate limit exceeded." },
                    ["500"] = new OpenApiResponse { Description = "Internal Server Error." }
                }
            });
    }

    private async Task<IResult> CreateCategory
    (
        CreateCategoryRequest request,
        IMediator mediator,
        IValidator<CreateCategoryRequest> validator
    )
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var response = await mediator.Send(request.Adapt<CreateCategoryCommand>());
        return response.Match
        (
            _ => Results.Created(),
            error => Problem(error)
        );
    }
}
