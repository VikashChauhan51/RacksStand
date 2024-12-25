using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Pocket.DeletePocket;
public record DeletePocketCommand(
    string Id
) : ICommand<Result<Unit>>;
