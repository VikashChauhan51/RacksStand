using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Shops.DeleteShop;
public class DeleteShopCommandHandler : ICommandHandler<DeleteShopCommand, Result<Unit>>
{
   
    public async Task<Result<Unit>> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
    {
        return Result<Unit>.Fail(new Exception("Shop not found."));
    }
}
