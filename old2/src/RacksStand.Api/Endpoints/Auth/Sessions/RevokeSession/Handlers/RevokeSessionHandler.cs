namespace RacksStand.Api.Endpoints.Auth.Sessions.RevokeSession.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;
using RacksStand.Api.Endpoints.Auth.Sessions.RevokeSession.Models;
using ResultifyCore;

public sealed class RevokeSessionHandler : ICommandHandler<RevokeSessionRequest, Outcome>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRefreshTokenReadRepository _refreshTokenReadRepository;
    private readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
    private readonly ILogger<RevokeSessionHandler> _logger;

    public RevokeSessionHandler(IHttpContextAccessor httpContextAccessor,
        IRefreshTokenReadRepository refreshTokenReadRepository,
        IRefreshTokenWriteRepository refreshTokenWriteRepository,
        ILogger<RevokeSessionHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _refreshTokenReadRepository = refreshTokenReadRepository;
        _refreshTokenWriteRepository = refreshTokenWriteRepository;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(RevokeSessionRequest command, CancellationToken ct)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("Revoke session attempted without authenticated user.");
            return Outcome.Unauthorized(new OutcomeError("Unauthorized", "User is not authenticated."));
        }

        var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Outcome.Problem(new OutcomeError("InvalidToken", "Access token missing subject."));
        }

        var token = await _refreshTokenReadRepository.GetByIdAsync(command.SessionId, ct);
        if (token == null)
        {
            return Outcome.NotFound(new OutcomeError("NotFound", "Session not found."));
        }

        if (token.UserId != userId)
        {
            return Outcome.Forbidden(new OutcomeError("Forbidden", "Cannot revoke session for another user."));
        }

        await _refreshTokenWriteRepository.RevokedAsync(token.Id, ct);
        _logger.LogInformation("Revoked session {SessionId} for user {UserId}", token.Id, userId);
        return Outcome.Success();
    }
}
