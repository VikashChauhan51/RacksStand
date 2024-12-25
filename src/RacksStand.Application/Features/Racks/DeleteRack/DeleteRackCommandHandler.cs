using MediatorForge.CQRS.Commands;


namespace RacksStand.Application.Features.Racks.DeleteRack;
public class DeleteRackCommandHandler : ICommandHandler<DeleteRackCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(DeleteRackCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

