using MediatorForge.CQRS.Commands;


namespace RacksStand.Application.Features.Shops.DeleteShop;
public record DeleteShopCommand(
    string Id
) : ICommand<Result<Unit>>;

