using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Racks.DeleteRack;
public record DeleteRackCommand(
    string Id
) : ICommand<Result<Unit>>;

