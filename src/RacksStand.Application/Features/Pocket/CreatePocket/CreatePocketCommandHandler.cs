using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Pocket.CreatePocket;
public class CreatePocketCommandHandler : ICommandHandler<CreatePocketCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(CreatePocketCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("Handler not implemented");
    }
}
