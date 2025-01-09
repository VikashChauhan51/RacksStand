using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Suppliers;

public class UpdateSupplierEndpoint : EndpointBase
{
    public UpdateSupplierEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "updateSupplier";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut<IResult>("/supplier/{supplierId}", CreateSupplierAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<NotFound>(StatusCodes.Status404NotFound)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Update supplier endpoint",
                Description = "Get a user's all assigned copanies.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> CreateSupplierAsync(
        [FromRoute] string supplierId,
        SupplierRequest request,
        IValidator<SupplierRequest> validator,
        ILogger<UpdateSupplierEndpoint> logger,
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

        if (string.IsNullOrEmpty(supplierId))
        {
            return Results.BadRequest();
        }
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var supplier = await dbContext.Suppliers.FindAsync(supplierId);
        if (supplier == null)
        {
            logger.LogError("The Supplier not found for id:{SupplierId}", supplierId);
            return Results.NotFound();
        }

        supplier.Name = request.Name;
        supplier.Code = request.Code;
        supplier.CompanyId = user.SelectedCompanyId;
        supplier.Description = request.Description;
        supplier.AddressLine1 = request.Address.Address;
        supplier.AddressLine2 = request.Address.Address;
        supplier.City = request.Address.City;
        supplier.State = request.Address.State;
        supplier.Country = request.Address.Country;
        supplier.ZipCode = request.Address.ZipCode;
        supplier.BillingAddressLine1 = request.BillingAddress.Address;
        supplier.BillingAddressLine2 = request.BillingAddress.Address2;
        supplier.BillingCity = request.BillingAddress.City;
        supplier.BillingCountry = request.BillingAddress.Country;
        supplier.BillingState = request.BillingAddress.State;
        supplier.BillingZipCode = request.BillingAddress.ZipCode;

        dbContext.Suppliers.Update(supplier);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}
