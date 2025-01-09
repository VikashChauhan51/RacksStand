using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Domain.Entities;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Suppliers;

public class CreateSupplierEndpoint : EndpointBase
{
    public CreateSupplierEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "createSupplier";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost<IResult>("/supplier", CreateSupplierAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Create supplier endpoint",
                Description = "Get a user's all assigned copanies.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> CreateSupplierAsync(
        SupplierRequest request,
        IValidator<SupplierRequest> validator,
        ILogger<CreateSupplierEndpoint> logger,
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

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var supplier = new Supplier
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Code = request.Code,
            CompanyId = user.SelectedCompanyId,
            Description = request.Description,
            AddressLine1 = request.Address.Address,
            AddressLine2 = request.Address.Address,
            City = request.Address.City,
            State = request.Address.State,
            Country = request.Address.Country,
            ZipCode = request.Address.ZipCode,
            BillingAddressLine1 = request.BillingAddress.Address,
            BillingAddressLine2 = request.BillingAddress.Address2,
            BillingCity = request.BillingAddress.City,
            BillingCountry = request.BillingAddress.Country,
            BillingState = request.BillingAddress.State,
            BillingZipCode = request.BillingAddress.ZipCode
        };
        dbContext.Suppliers.Add(supplier);
        await dbContext.SaveChangesAsync();

        return Results.Created();
    }
}
