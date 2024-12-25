using MediatorForge.CQRS.Commands;


namespace RacksStand.Application.Features.Shops.UpdateShop;
public class UpdateShopCommandHandler : ICommandHandler<UpdateShopCommand, Result<Unit>>
{

    public async Task<Result<Unit>> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
    {
        return Result<Unit>.Fail(new Exception("Shop not found."));
    }
}
