namespace RacksStand.Api.Domain.Repositories.Tokens;

using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;


public sealed class RefreshTokenReadRepository : IRefreshTokenReadRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly ILogger<RefreshTokenReadRepository> _logger;

    public RefreshTokenReadRepository(
        ILogger<RefreshTokenReadRepository> logger,
        IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task<RefreshToken?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting RefreshToken by Id: {Id}", id);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.RefreshTokens.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<RefreshToken?> GetByIdWithUserAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting RefreshToken with User by Id: {Id}", id);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.RefreshTokens.AsNoTracking()
            .AsSplitQuery()
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting RefreshToken by TokenHash: {TokenHash}", tokenHash);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.RefreshTokens.AsNoTracking()
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);
    }
    public async Task<RefreshToken?> GetByTokenHashWithUserAsync(string tokenHash, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting RefreshToken with User by TokenHash: {TokenHash}", tokenHash);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.RefreshTokens.AsNoTracking()
             .AsSplitQuery()
             .Include(rt => rt.User)
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash, cancellationToken);
    }

    public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting RefreshTokens for user {UserId}", userId);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.RefreshTokens.AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}

