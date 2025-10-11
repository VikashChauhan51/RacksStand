using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Products;

public class UpdateProductEndpoint : EndpointBase
{
    public UpdateProductEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "updateProduct";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut<IResult>("/{categoryId}/product/{productId}", UpdateProductAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Update product endpoint",
                Description = "Get a user's all assigned copanies.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> UpdateProductAsync(
        [FromRoute] string categoryId,
        [FromRoute] string productId,
        UpdateProductRequest request,
        IValidator<UpdateProductRequest> validator,
        ILogger<UpdateProductEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext)
    {
        var userName = httpContext.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            return Results.Unauthorized();
        }

        var user = await dbContext.Users
            .AsNoTracking().
            Where(x => x.UserName == userName)
            .Select(x => new { x.SelectedCompanyId, x.Id })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            logger.LogWarning("User not found for username clam: {UserName}", userName);
            return Results.Unauthorized();
        }

        if (string.IsNullOrEmpty(categoryId))
        {

            return Results.BadRequest();
        }

        if (string.IsNullOrEmpty(productId))
        {

            return Results.BadRequest();
        }

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var existingProduct = await dbContext.Products
                    .FirstOrDefaultAsync(p => p.Id == productId);

        if (existingProduct == null)
        {
            return Results.NotFound();
        }

        // Step 1: Update product details
        existingProduct.Name = request.Name;
        existingProduct.CategoryId = categoryId;
        existingProduct.Description = request.Description;
        existingProduct.Brand = request.Brand;
        existingProduct.Type = request.Type;
        existingProduct.Tags = request.Tags;
        existingProduct.Metadata = request.Metadata;

        // Save changes and commit transaction
        await dbContext.SaveChangesAsync();
        return Results.NoContent();
    }
}
