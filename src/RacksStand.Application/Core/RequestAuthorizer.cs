using MediatorForge.CQRS.Authorizers;
using MediatorForge.Utilities;


namespace RacksStand.Application.Core;
public class RequestAuthorizer<TRequest> : IAuthorizer<TRequest>
{
    public Task<AuthorizationResult> AuthorizeAsync(TRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        return Task.FromResult(AuthorizationResult.Success);
    }
}
