namespace RacksStand.Api.Endpoints.Auth.Logout.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;
using RacksStand.Api.Endpoints.Auth.Logout.Models;
using ResultifyCore;

public sealed class LogoutHandler : ICommandHandler<LogoutRequest, Outcome>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAccessTokenWriteRepository _accessTokenWriteRepository;
    private readonly ILogger<LogoutHandler> _logger;

    public LogoutHandler(
        IHttpContextAccessor httpContextAccessor,
        IAccessTokenWriteRepository accessTokenWriteRepository,
        ILogger<LogoutHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _accessTokenWriteRepository = accessTokenWriteRepository;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(LogoutRequest command, CancellationToken ct)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("Logout attempted without authenticated user.");
            return Outcome.Unauthorized(new OutcomeError("Unauthorized", "User is not authenticated."));
        }

        var jti = user.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
        if (string.IsNullOrWhiteSpace(jti))
        {
            _logger.LogWarning("Logout attempted but no jti claim found on user principal.");
            return Outcome.Problem(new OutcomeError("InvalidToken", "Access token does not contain jti claim."));
        }

        await _accessTokenWriteRepository.RevokedAsync(jti, ct);
        _logger.LogInformation("Access token with id {Jti} revoked on logout.", jti);
        return Outcome.Success();
    }
}
