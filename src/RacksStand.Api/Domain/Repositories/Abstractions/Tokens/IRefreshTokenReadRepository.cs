namespace RacksStand.Api.Domain.Repositories.Abstractions.Tokens;

using RacksStand.Api.Domain.Entities;

/// <summary>
/// Provides read-only operations for <see cref="RefreshToken"/> entities.
/// </summary>
public interface IRefreshTokenReadRepository
{
    /// <summary>
    /// Retrieves a <see cref="RefreshToken"/> by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the refresh token.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation if needed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <see cref="RefreshToken"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<RefreshToken?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a <see cref="RefreshToken"/> by its unique identifier, including the associated user.
    /// </summary>
    /// <param name="id">The unique identifier of the refresh token.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation if needed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <see cref="RefreshToken"/> with the associated user if found; otherwise, <c>null</c>.
    /// </returns>
    Task<RefreshToken?> GetByIdWithUserAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a <see cref="RefreshToken"/> by its token hash.
    /// </summary>
    /// <param name="tokenHash">The hash of the refresh token.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation if needed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <see cref="RefreshToken"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a <see cref="RefreshToken"/> by its token hash, including the associated user.
    /// </summary>
    /// <param name="tokenHash">The hash of the refresh token.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation if needed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <see cref="RefreshToken"/> with the associated user if found; otherwise, <c>null</c>.
    /// </returns>
    Task<RefreshToken?> GetByTokenHashWithUserAsync(string tokenHash, CancellationToken cancellationToken = default);
}

