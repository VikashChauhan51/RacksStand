using MediatorForge.CQRS.Commands;
using RacksStand.Domain.Entities;

namespace RacksStand.Application.Features.Shops.CreateShop;
public class CreateShopCommandHandler : ICommandHandler<CreateShopCommand, Result<Unit>>
{
     

    public async Task<Result<Unit>> Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {
        // Business logic to create a shop
        var newShop = new Shop
        {
            Name = request.Name,
            Address = request.Address,
            Description = request.Description
        };

        // Return success result with response
        return new Result<Unit>(Unit.Value);
    }
}

