using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;
using ResultifyCore.Outcome;

namespace RacksStand.Api.Features.Suppliers;

public class GetSuppliersEndpoint : EndpointBase
{
    public GetSuppliersEndpoint()
    {
        RequireAuthorization();
    }

    public override string RouteName => "getSuppliers";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/suppliers", GetSuppliersAsync)
            .WithName(RouteName)
            .Produces<PaginatedResponse<GetSuppliersResponse>>(StatusCodes.Status200OK)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Get suppliers endpoint",
                Description = "Retrieve a list of suppliers for the user's assigned company with filtering and pagination.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();
    }

    private static async Task<IResult> GetSuppliersAsync(
        ILogger<GetSuppliersEndpoint> logger,
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

        var query = dbContext.Suppliers
            .Where(x => x.CompanyId == user.SelectedCompanyId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.Name.Contains(search) || x.Code.Contains(search));
        }

        var totalItems = await query.CountAsync(); // Total suppliers count for pagination

        var suppliers = await query
            .OrderBy(x => x.Name) // Sort by Name (you can modify sorting criteria as needed)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new GetSuppliersResponse(
                x.Id.Value,
                x.Name,
                x.Code))
            .ToListAsync();

        var paginatedResponse = new PaginatedResponse<GetSuppliersResponse>(
            suppliers,
            page,
            pageSize,
            totalItems);

        return Results.Ok(paginatedResponse);
    }
}
