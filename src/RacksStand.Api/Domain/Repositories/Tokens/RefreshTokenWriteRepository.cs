namespace RacksStand.Api.Domain.Repositories.Tokens;

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
}
