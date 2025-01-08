using Microsoft.AspNetCore.Mvc;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;
using ResultifyCore.Outcome;
using System.Security.Claims;

namespace RacksStand.Api.Features.GetCompany;

public class GetCompanyEndpoint : EndpointBase
{
    public GetCompanyEndpoint()
    {
        this.RequireAuthorization();
    }
    public override string RouteName => "getCompany";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/companies/{companyId}", GetCompanyAsync)
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
        [FromRoute] string companyId,
        ILogger<GetCompanyEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext)
    {
        if (string.IsNullOrWhiteSpace(companyId))
        {
            return Results.BadRequest("Invalid companyId");
        }
        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Results.Unauthorized();
        }

        var user = await dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            logger.LogWarning("User not found for NameIdentifier clam: {Identifier}", userId);
            return Results.NotFound(Outcome<GetCompanyResponse>.Failure(new OutcomeError("UserNotFound", "The user associated with this token does not exist.")));
        }

        var company = await dbContext.Companies.FindAsync(companyId);

        if (company == null || !company.IsActive)
        {
            logger.LogWarning("Company not found for id: {CompanyId}", companyId);
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

