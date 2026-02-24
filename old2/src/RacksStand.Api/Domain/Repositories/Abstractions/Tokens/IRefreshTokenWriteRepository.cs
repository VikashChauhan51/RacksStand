using RacksStand.Api.Domain.Entities;

namespace RacksStand.Api.Domain.Repositories.Abstractions.Tokens;

public interface IRefreshTokenWriteRepository
{
    Task<RefreshToken?> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
    Task  RevokedAsync(string refreshTokenId, CancellationToken cancellationToken = default);
    Task RevokedAllForUserAsync(string userId, CancellationToken cancellationToken = default);
}
