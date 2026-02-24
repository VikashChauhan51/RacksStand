namespace RacksStand.Api.Endpoints.Auth.Mfa.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.Mfa.Models;
using ResultifyCore;

public sealed class VerifyAuthenticatorHandler : ICommandHandler<VerifyAuthenticatorRequest, Outcome>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<VerifyAuthenticatorHandler> _logger;

    public VerifyAuthenticatorHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ILogger<VerifyAuthenticatorHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(VerifyAuthenticatorRequest command, CancellationToken ct)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("VerifyAuthenticator attempted without authenticated user.");
            return Outcome.Unauthorized(new OutcomeError("Unauthorized", "User is not authenticated."));
        }

        var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Outcome.Problem(new OutcomeError("InvalidToken", "Access token missing subject."));
        }

        var appUser = await _userManager.FindByIdAsync(userId);
        if (appUser == null)
        {
            return Outcome.NotFound(new OutcomeError("UserNotFound", "User not found."));
        }

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(appUser, TokenOptions.DefaultAuthenticatorProvider, command.Code);
        if (!isValid)
        {
            return Outcome.Unauthorized(new OutcomeError("InvalidCode", "Code is invalid."));
        }

        var result = await _userManager.SetTwoFactorEnabledAsync(appUser, true);
        if (!result.Succeeded)
        {
            return Outcome.Problem(new OutcomeError("Enable2FAFailed", "Failed to enable two-factor authentication."));
        }

        _logger.LogInformation("Two-factor authenticator enabled for user {UserId}", userId);
        return Outcome.Success();
    }
}
