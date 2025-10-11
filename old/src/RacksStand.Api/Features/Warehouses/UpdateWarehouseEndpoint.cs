using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Domain.Enums;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Warehouses;

public class UpdateWarehouseEndpoint : EndpointBase
{
    public UpdateWarehouseEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "updateWarehouse";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut<IResult>("/warehouse/{warehouseId}", CreateWarehouseAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<NotFound>(StatusCodes.Status404NotFound)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Update warehouse endpoint",
                Description = "Get a user's all assigned copanies.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> CreateWarehouseAsync(
        [FromRoute] string warehouseId,
        WarehouseRequest request,
        IValidator<WarehouseRequest> validator,
        ILogger<UpdateWarehouseEndpoint> logger,
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

        if (string.IsNullOrEmpty(warehouseId))
        {
            return Results.BadRequest();
        }
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var warehouse = await dbContext.Warehouses.FindAsync(warehouseId);
        if (warehouse == null)
        {
            logger.LogError("The warehouse not found for id:{WarehouseId}", warehouseId);
            return Results.NotFound();
        }

        warehouse.Name = request.Name;
        warehouse.Code = request.Code;
        warehouse.ManagerId = request.ManagerId;
        warehouse.CompanyId = user.SelectedCompanyId;
        warehouse.Description = request.Description;
        warehouse.PhoneNumber = request.PhoneNumber;
        warehouse.Height = request.Height;
        warehouse.Width = request.Width;
        warehouse.Length = request.Length;
        warehouse.DiminsionUnit = Enum.Parse<DimensionUnit>(request.DimensionUnit, true);
        warehouse.AddressLine1 = request.Address.Address;
        warehouse.AddressLine2 = request.Address.Address;
        warehouse.City = request.Address.City;
        warehouse.State = request.Address.State;
        warehouse.Country = request.Address.Country;
        warehouse.ZipCode = request.Address.ZipCode;
        warehouse.ReturnAddressLine1 = request.ReturnAddress.Address;
        warehouse.ReturnAddressLine2 = request.ReturnAddress.Address2;
        warehouse.ReturnCity = request.ReturnAddress.City;
        warehouse.ReturnCountry = request.ReturnAddress.Country;
        warehouse.ReturnState = request.ReturnAddress.State;
        warehouse.ReturnZipCode = request.ReturnAddress.ZipCode;

        dbContext.Warehouses.Update(warehouse);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}
