using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Racks.UpdateRack;
public class UpdateRackCommandHandler : ICommandHandler<UpdateRackCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(UpdateRackCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();    
    }
}

