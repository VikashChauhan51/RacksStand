namespace RacksStand.Api.Domain.Repositories.Abstractions.Tokens;

using RacksStand.Api.Domain.Entities;

/// <summary>
/// Provides read-only operations for <see cref="AccessToken"/> entities.
/// </summary>
public interface IAccessTokenReadRepository
{
    /// <summary>
    /// Retrieves an <see cref="AccessToken"/> by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the access token.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation if needed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <see cref="AccessToken"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<AccessToken?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an <see cref="AccessToken"/> by its unique identifier, including the associated user.
    /// </summary>
    /// <param name="id">The unique identifier of the access token.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation if needed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <see cref="AccessToken"/> with the associated user if found; otherwise, <c>null</c>.
    /// </returns>
    Task<AccessToken?> GetByIdWithUserAsync(string id, CancellationToken cancellationToken = default);
}

