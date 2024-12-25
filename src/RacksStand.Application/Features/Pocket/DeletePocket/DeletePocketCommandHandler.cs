using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Pocket.DeletePocket;
public class DeletePocketCommandHandler : ICommandHandler<DeletePocketCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(DeletePocketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}

