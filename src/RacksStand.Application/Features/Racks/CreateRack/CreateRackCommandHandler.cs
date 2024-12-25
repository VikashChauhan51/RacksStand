using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Racks.CreateRack;
public class CreateRackCommandHandler : ICommandHandler<CreateRackCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(CreateRackCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();    
    }
}

