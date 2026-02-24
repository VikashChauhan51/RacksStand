using RacksStand.Api.Domain.Entities;

namespace RacksStand.Api.Domain.Repositories.Abstractions.Tokens;

public interface IAccessTokenWriteRepository
{
    Task<AccessToken?> AddAsync(AccessToken accessToken, CancellationToken cancellationToken = default);
    Task RevokedAsync(string accessTokenId, CancellationToken cancellationToken = default);
    Task RevokedAllForUserAsync(string userId, CancellationToken cancellationToken = default);
}
