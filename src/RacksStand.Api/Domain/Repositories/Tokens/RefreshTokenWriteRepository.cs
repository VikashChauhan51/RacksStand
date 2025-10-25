namespace RacksStand.Api.Domain.Repositories.Tokens;

using Azure.Core;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;


public sealed class RefreshTokenWriteRepository : IRefreshTokenWriteRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RefreshTokenWriteRepository> _logger;
    public RefreshTokenWriteRepository(
        ILogger<RefreshTokenWriteRepository> logger,
        ApplicationDbContext context
        )
    {
        _logger = logger;
        _context = context;
        
    }
    public async Task<RefreshToken?> AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(refreshToken, nameof(refreshToken));
        _logger.LogInformation("Adding refresh token for user {UserId}", refreshToken.UserId);
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
        return refreshToken;
    }

    public async Task RevokedAsync(string refreshTokenId, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(refreshTokenId, nameof(refreshTokenId));

        var token = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Id == refreshTokenId);
        if (token == null)
        {
            _logger.LogWarning("Access token with ID {RefreshTokenId} not found for revocation", refreshTokenId);
            return;
        }

        token.DeletedAt = DateTimeOffset.UtcNow;
        _context.RefreshTokens.Update(token);
        await _context.SaveChangesAsync(cancellationToken);

    }
}
