namespace RacksStand.Api.Endpoints.Auth.LogoutAll.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;
using RacksStand.Api.Endpoints.Auth.LogoutAll.Models;
using ResultifyCore;

public sealed class LogoutAllHandler : ICommandHandler<LogoutAllRequest, Outcome>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
    private readonly IAccessTokenWriteRepository _accessTokenWriteRepository;
    private readonly ILogger<LogoutAllHandler> _logger;

    public LogoutAllHandler(IHttpContextAccessor httpContextAccessor,
        IRefreshTokenWriteRepository refreshTokenWriteRepository,
        IAccessTokenWriteRepository accessTokenWriteRepository,
        ILogger<LogoutAllHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _refreshTokenWriteRepository = refreshTokenWriteRepository;
        _accessTokenWriteRepository = accessTokenWriteRepository;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(LogoutAllRequest command, CancellationToken ct)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("LogoutAll attempted without authenticated user.");
            return Outcome.Unauthorized(new OutcomeError("Unauthorized", "User is not authenticated."));
        }

        var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Outcome.Problem(new OutcomeError("InvalidToken", "Access token missing subject."));
        }

        await _refreshTokenWriteRepository.RevokedAllForUserAsync(userId, ct);
        await _accessTokenWriteRepository.RevokedAllForUserAsync(userId, ct);
        _logger.LogInformation("All sessions revoked for user {UserId}", userId);
        return Outcome.Success();
    }
}
