using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Shops.CreateShop;
public record CreateShopCommand(
    string Name,
    string Address,
    string? Description
) : ICommand<Result<Unit>>;

