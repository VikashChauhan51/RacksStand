namespace RacksStand.Api.Endpoints.Auth.ChangePassword.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.ChangePassword.Models;
using ResultifyCore;

public sealed class ChangePasswordHandler : ICommandHandler<ChangePasswordRequest, Outcome>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ChangePasswordHandler> _logger;

    public ChangePasswordHandler(
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager,
        ILogger<ChangePasswordHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(ChangePasswordRequest command, CancellationToken ct)
    {
        var userPrincipal = _httpContextAccessor.HttpContext?.User;
        if (userPrincipal == null || !userPrincipal.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("Change password attempted without authenticated user.");
            return Outcome.Unauthorized(new OutcomeError("Unauthorized", "User is not authenticated."));
        }

        var userId = userPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogWarning("Change password attempted but no subject claim found.");
            return Outcome.Problem(new OutcomeError("InvalidToken", "Access token does not contain subject claim."));
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || user.DeletedAt != null)
        {
            _logger.LogWarning("User not found or deleted for change password (id: {UserId}).", userId);
            return Outcome.NotFound(new OutcomeError("UserNotFound", "User not found."));
        }

        var result = await _userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);
        if (result.Succeeded)
        {
            _logger.LogInformation("Password changed for user {UserId}.", userId);
            return Outcome.Success();
        }

        _logger.LogWarning("Password change failed for user {UserId}.", userId);
        return Outcome.Validation(result.Errors.Select(e => new OutcomeError(e.Code, e.Description)));
    }
}
