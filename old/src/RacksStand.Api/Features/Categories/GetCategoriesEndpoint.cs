using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Api.Features.Categories;
using RacksStand.Infrastructure.Data.Contexts;

public class GetCategoriesEndpoint : EndpointBase
{
    public GetCategoriesEndpoint()
    {
        RequireAuthorization();
    }

    public override string RouteName => "getCategories";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/categories", GetCategoriesAsync)
            .WithName(RouteName)
            .Produces<PaginatedResponse<GetCategoriesResponse>>(StatusCodes.Status200OK)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Get categories endpoint",
                Description = "Retrieve a list of categories for the user's assigned company with filtering and pagination.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();
    }

    private static async Task<IResult> GetCategoriesAsync(
        ILogger<GetCategoriesEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null)
    {
        var userName = httpContext.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            return Results.Unauthorized();
        }

        var user = await dbContext.Users
            .AsNoTracking()
            .Where(x => x.UserName == userName)
            .Select(x => new { x.SelectedCompanyId, x.Id })
            .FirstOrDefaultAsync();

        if (user == null)
        {
            logger.LogWarning("User not found for username claim: {UserName}", userName);
            return Results.Unauthorized();
        }

        if (page < 1 || pageSize < 1)
        {
            return Results.BadRequest("Page and pageSize must be greater than 0.");
        }

        var query = dbContext.Categories
            .Where(x => x.CompanyId == user.SelectedCompanyId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.Name.Contains(search));
        }

        var totalItems = await query.CountAsync(); // Total categories count for pagination

        var categories = await query
            .OrderBy(x => x.Name) // Sort by Name (modify sorting criteria if needed)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new GetCategoriesResponse(
                x.Id.Value,
                x.Name))
            .ToListAsync();

        var paginatedResponse = new PaginatedResponse<GetCategoriesResponse>(
            categories,
            page,
            pageSize,
            totalItems);

        return Results.Ok(paginatedResponse);
    }
}
