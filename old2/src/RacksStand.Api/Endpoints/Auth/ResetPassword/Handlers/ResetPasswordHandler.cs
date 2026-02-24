namespace RacksStand.Api.Endpoints.Auth.ResetPassword.Handlers;

using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Identity;
using FastEndpoints;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.ResetPassword.Models;
using RacksStand.Api.Core.Logging;
using ResultifyCore;

public sealed class ResetPasswordHandler : ICommandHandler<ResetPasswordRequest, Outcome>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ResetPasswordHandler> _logger;

    public ResetPasswordHandler(UserManager<ApplicationUser> userManager, ILogger<ResetPasswordHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(ResetPasswordRequest command, CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
        {
            _logger.LogWarning("Reset password attempted for non-existing user {UserId}", LogSanitizer.SanitizeAndMask(command.UserId));
            // Do not reveal too much - but returning NotFound keeps behavior explicit
            return Outcome.NotFound(new OutcomeError("UserNotFound", "User not found."));
        }

        var decoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(command.Code));
        var result = await _userManager.ResetPasswordAsync(user, decoded, command.NewPassword);
        if (result.Succeeded)
        {
            _logger.LogInformation("Password reset successfully for user {UserId}", user.Id);
            return Outcome.Success();
        }

        _logger.LogWarning("Password reset failed for user {UserId}", user.Id);
        return Outcome.Validation(result.Errors.Select(e => new OutcomeError(e.Code, e.Description)));
    }
}
