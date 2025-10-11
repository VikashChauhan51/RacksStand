using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;
using ResultifyCore.Outcome;

namespace RacksStand.Api.Features.Companies;

public class GetCompanyEndpoint : EndpointBase
{
    public GetCompanyEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "getCompany";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/company", GetCompanyAsync)
            .WithName(RouteName)
            .Produces<Outcome<GetCompanyResponse>>(StatusCodes.Status200OK)
            .Produces<Outcome<GetCompanyResponse>>(StatusCodes.Status400BadRequest)
            .Produces<Outcome<GetCompanyResponse>>(StatusCodes.Status404NotFound)
            .WithMetadata(new
            {
                Summary = "Company profile Endpoint",
                Description = "Get a user's selected company.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> GetCompanyAsync(
        ILogger<GetCompanyEndpoint> logger,
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
            return Results.Unauthorized();
        }

        var company = await dbContext.Companies.FindAsync(user.SelectedCompanyId);

        if (company == null || !company.IsActive)
        {
            logger.LogWarning("Company not found for id: {CompanyId}", user.SelectedCompanyId);
            return Results.NotFound(Outcome<GetCompanyResponse>.Failure(new OutcomeError("CompanyNotFound", "The company associated with this user does not exist.")));

        }
        return Results.Ok(Outcome<GetCompanyResponse>.Success(new GetCompanyResponse
        (
            Id: company.Id.Value,
            Name: company.Name,
            Description: company.Description,
            AddressLine1: company.AddressLine1,
            AddressLine2: company.AddressLine2,
            State: company.State,
            City: company.City,
            Country: company.Country,
            ZipCode: company.ZipCode,
            DefaultDimensionUnit: company.DefaultDimensionUnit.ToString(),
            DefaultWeightUnit: company.DefaultWeightUnit.ToString(),
            DefaultCurrency: company.DefaultCurrency.ToString(),
            DefaultItemBarcodeFormat: company.DefaultItemBarcodeFormat.ToString(),
            DefaultBinBarcodeFormat: company.DefaultBinBarcodeFormat.ToString(),
            DefaultShipmentBarcodeFormat: company.DefaultShipmentBarcodeFormat.ToString(),
            IsOwner: company.OwnerUserId == user.Id
        )));
    }
}

