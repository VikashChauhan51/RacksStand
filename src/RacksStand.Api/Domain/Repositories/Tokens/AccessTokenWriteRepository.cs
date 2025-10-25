namespace RacksStand.Api.Domain.Repositories.Tokens;

using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;

public sealed class AccessTokenWriteRepository : IAccessTokenWriteRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AccessTokenWriteRepository> _logger;
    public AccessTokenWriteRepository(
        ILogger<AccessTokenWriteRepository> logger,
        ApplicationDbContext context
        )
    {
        _logger = logger;
        _context = context;

    }
    public async Task<AccessToken?> AddAsync(AccessToken accessToken, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(accessToken, nameof(accessToken));
        _logger.LogInformation("Adding access token for user {UserId}", accessToken.UserId);
        _context.AccessTokens.Add(accessToken);
        await _context.SaveChangesAsync();
        return accessToken;
    }

    public async Task RevokedAsync(string accessTokenId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(accessTokenId, nameof(accessTokenId));

        var token = await _context.AccessTokens.FirstOrDefaultAsync(x => x.Id == accessTokenId);
        if (token == null)
        {
            _logger.LogWarning("Access token with ID {AccessTokenId} not found for revocation", accessTokenId);
            return;
        }

        token.DeletedAt = DateTimeOffset.UtcNow;
        _context.AccessTokens.Update(token);
        await _context.SaveChangesAsync(cancellationToken);

    }

    public async Task RevokedAllForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));
        _logger.LogInformation("Revoking all access tokens for user {UserId}", userId);
        var tokens = await _context.AccessTokens.Where(x => x.UserId == userId && x.DeletedAt == null).ToListAsync(cancellationToken);
        if (!tokens.Any())
        {
            _logger.LogInformation("No active access tokens found for user {UserId}", userId);
            return;
        }
        foreach (var t in tokens)
        {
            t.DeletedAt = DateTimeOffset.UtcNow;
        }
        _context.AccessTokens.UpdateRange(tokens);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
