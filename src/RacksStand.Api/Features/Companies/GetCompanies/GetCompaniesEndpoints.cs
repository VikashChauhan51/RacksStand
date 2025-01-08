using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;
using ResultifyCore.Outcome;

namespace RacksStand.Api.Features.Companies.GetCompanies;

public class GetCompaniesEndpoints : EndpointBase
{
    public GetCompaniesEndpoints()
    {
        RequireAuthorization();
    }
    public override string RouteName => "getCompanies";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/companies", GetCompanyAsync)
            .WithName(RouteName)
            .Produces<Outcome<IEnumerable<GetCompaniesResponse>>>(StatusCodes.Status200OK)
            .Produces<Outcome<IEnumerable<GetCompaniesResponse>>>(StatusCodes.Status400BadRequest)
            .Produces<Outcome<IEnumerable<GetCompaniesResponse>>>(StatusCodes.Status404NotFound)
            .WithMetadata(new
            {
                Summary = "Companies Endpoint",
                Description = "Get a user's all assigned copanies.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> GetCompanyAsync(
        ILogger<GetCompaniesEndpoints> logger,
        RacksStandContext dbContext,
        HttpContext httpContext)
    {
        var userName = httpContext.GetUserName();
        if (string.IsNullOrEmpty(userName))
        {
            return Results.Unauthorized();
        }

        var userCompanies = await dbContext.Users
                .Include(x => x.Companies)
                .ThenInclude(x => x.Company)
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.UserName == userName)
                .SelectMany(x => x.Companies)
                .Select(c => new GetCompaniesResponse(c.Company.Id.Value, c.Company.Name))
                .ToListAsync();

        if (userCompanies == null)
        {
            logger.LogWarning("Company not found for user: {Username}", userName);
            return Results.NotFound
            (
                Outcome<IEnumerable<GetCompaniesResponse>>.Failure
                (
                    new OutcomeError("CompaniesNotFound", "The companies associated with this user does not exist.")
                )
            );

        }
        return Results.Ok(Outcome<IEnumerable<GetCompaniesResponse>>.Success(userCompanies));
    }
}
