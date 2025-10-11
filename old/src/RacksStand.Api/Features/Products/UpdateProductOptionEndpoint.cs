using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Products;

public class UpdateProductOptionEndpoint : EndpointBase
{
    public UpdateProductOptionEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "updateProductOption";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut<IResult>("/product/{productId}/option/{optionId}", UpdateProductOptionAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithOpenApi();
    }

    private static async Task<IResult> UpdateProductOptionAsync(
        [FromRoute] string productId,
        [FromRoute] string optionId,
        ProductOptionRequest request,
        IValidator<ProductOptionRequest> validator,
        ILogger<UpdateProductOptionEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext)
    {
        var userName = httpContext.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            return Results.Unauthorized();
        }

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var existingOption = await dbContext.ProductOptions
            .FirstOrDefaultAsync(o => o.Id == optionId && o.ProductId == productId);

        if (existingOption == null)
        {
            return Results.NotFound();
        }

        existingOption.Name = request.Name;

        await dbContext.SaveChangesAsync();
        return Results.NoContent();
    }
}
