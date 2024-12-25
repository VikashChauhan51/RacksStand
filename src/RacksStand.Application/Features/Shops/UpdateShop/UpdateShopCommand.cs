using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Shops.UpdateShop;
public record UpdateShopCommand(
    string Id,
    string Name,
    string Address,
    string? Description
) : ICommand<Result<Unit>>;

