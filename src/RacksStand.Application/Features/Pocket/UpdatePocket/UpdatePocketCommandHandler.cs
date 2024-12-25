using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Pocket.UpdatePocket;
public class UpdatePocketCommandHandler : ICommandHandler<UpdatePocketCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(UpdatePocketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}

