using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core;
using RacksStand.Domain.Entities;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Racks;

public class CreateRackEndpoint : EndpointBase
{
    public CreateRackEndpoint()
    {
        RequireAuthorization();
    }

    public override string RouteName => "createRackWithShelvesAndBins";
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/{warehouseId}/racks", CreateRackWithShelvesAndBinsAsync)
            .WithName(RouteName)
            .Produces(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError)
            .Produces<UnauthorizedHttpResult>(StatusCodes.Status401Unauthorized)
            .WithMetadata(new
            {
                Summary = "Create rack with shelves and bins endpoint",
                Description = "Create a rack along with its shelves and bins in one request.",
                Consumer = "Frontend Client",
                Producer = "Rack Management Service"
            })
            .WithOpenApi();
    }

    private static async Task<IResult> CreateRackWithShelvesAndBinsAsync(
        ILogger<CreateRackEndpoint> logger,
        RacksStandContext dbContext,
        HttpContext httpContext,
        [FromBody] CreateRackWithShelvesAndBinsRequest request,
        IValidator<CreateRackWithShelvesAndBinsRequest> validator,
        [FromRoute] string warehouseId)
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

        using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            // Create Rack
            var rack = new Rack
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Rack.Name,
                Code = request.Rack.Code,
                Label = request.Rack.Label,
                Description = request.Rack.Description,
                XAxis = request.Rack.XAxis,
                YAxis = request.Rack.YAxis,
                Height = request.Rack.Height,
                Width = request.Rack.Width,
                Length = request.Rack.Length,
                DiminsionUnit = Enum.Parse<DimensionUnit>(request.Rack.DiminsionUnit, true),
                WarehouseId = warehouse.Id,
                State = RackState.Empty
            };

            dbContext.Racks.Add(rack);

            // Create Shelves
            foreach (var shelfRequest in request.Rack.Shelves)
            {
                var shelf = new Self
                {
                    Id = Guid.NewGuid().ToString(),
                    RackId = rack.Id,
                    Name = shelfRequest.Name,
                    Code = shelfRequest.Code,
                    Label = shelfRequest.Label,
                    Level = shelfRequest.Level,
                    Description = shelfRequest.Description,
                    XAxis = shelfRequest.XAxis,
                    YAxis = shelfRequest.YAxis,
                    Height = shelfRequest.Height,
                    Width = shelfRequest.Width,
                    Length = shelfRequest.Length,
                    DiminsionUnit = Enum.Parse<DimensionUnit>(shelfRequest.DiminsionUnit, true),
                    State = SelfState.Empty
                };

                dbContext.Shelves.Add(shelf);

                // Create Bins
                foreach (var binRequest in shelfRequest.Bins)
                {
                    var bin = new Bin
                    {
                        Id = Guid.NewGuid().ToString(),
                        SelfId = shelf.Id,
                        Name = binRequest.Name,
                        Code = binRequest.Code,
                        Label = binRequest.Label,
                        Level = binRequest.Level,
                        Description = binRequest.Description,
                        Barcode = binRequest.Barcode != null ? Barcode.Of(binRequest.Barcode) : null,
                        BarcodeFormat = binRequest.BarcodeFormat != null ? Enum.Parse<BarcodeFormat>(binRequest.BarcodeFormat, true) : null,
                        XAxis = binRequest.XAxis,
                        YAxis = binRequest.YAxis,
                        Height = binRequest.Height,
                        Width = binRequest.Width,
                        Length = binRequest.Length,
                        DiminsionUnit = Enum.Parse<DimensionUnit>(binRequest.DiminsionUnit, true),
                        State = BinState.Empty
                    };

                    dbContext.Bins.Add(bin);

                }
            }
            await dbContext.SaveChangesAsync();
            // Commit transaction
            await transaction.CommitAsync();

        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

        return Results.Created();
    }
}
