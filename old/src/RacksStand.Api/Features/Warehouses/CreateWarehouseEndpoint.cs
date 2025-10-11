using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Domain.Entities;
using RacksStand.Domain.Enums;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Warehouses;

public class CreateWarehouseEndpoint : EndpointBase
{
    public CreateWarehouseEndpoint()
    {
        RequireAuthorization();
    }
    public override string RouteName => "createWarehouse";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost<IResult>("/warehouse", CreateWarehouseAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Create warehouse endpoint",
                Description = "Get a user's all assigned copanies.",
                Consumer = "Frontend Client",
                Producer = "Account Management Service"
            })
            .WithOpenApi();

    }

    private static async Task<IResult> CreateWarehouseAsync(
        WarehouseRequest request,
        IValidator<WarehouseRequest> validator,
        ILogger<CreateWarehouseEndpoint> logger,
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

        var warehouse = new Warehouse
        {
            Id = Guid.NewGuid().ToString(),
            Name = request.Name,
            Code = request.Code,
            ManagerId = request.ManagerId,
            CompanyId = user.SelectedCompanyId,
            Description = request.Description,
            PhoneNumber = request.PhoneNumber,
            Height = request.Height,
            Width = request.Width,
            Length = request.Length,
            DiminsionUnit = Enum.Parse<DimensionUnit>(request.DimensionUnit, true),
            AddressLine1 = request.Address.Address,
            AddressLine2 = request.Address.Address,
            City = request.Address.City,
            State = request.Address.State,
            Country = request.Address.Country,
            ZipCode = request.Address.ZipCode,
            ReturnAddressLine1 = request.ReturnAddress.Address,
            ReturnAddressLine2 = request.ReturnAddress.Address2,
            ReturnCity = request.ReturnAddress.City,
            ReturnCountry = request.ReturnAddress.Country,
            ReturnState = request.ReturnAddress.State,
            ReturnZipCode = request.ReturnAddress.ZipCode
        };
        dbContext.Warehouses.Add(warehouse);
        await dbContext.SaveChangesAsync();

        return Results.Created();
    }
}
