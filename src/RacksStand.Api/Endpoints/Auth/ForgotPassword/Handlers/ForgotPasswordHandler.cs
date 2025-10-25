namespace RacksStand.Api.Endpoints.Auth.ForgotPassword.Handlers;

using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using FastEndpoints;
using RacksStand.Api.Core.Logging;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.ForgotPassword.Models;
using ResultifyCore;

public sealed class ForgotPasswordHandler : ICommandHandler<ForgotPasswordRequest, Outcome>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<ForgotPasswordHandler> _logger;

    public ForgotPasswordHandler(
        UserManager<ApplicationUser> userManager,
        IEmailSender emailSender,
        ILogger<ForgotPasswordHandler> logger)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(ForgotPasswordRequest command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
        {
            return Outcome.Problem(new OutcomeError("InvalidRequest", "Email is required."));
        }

        var user = await _userManager.FindByEmailAsync(command.Email);
        // Do not reveal whether the user exists. Return success in all cases for security.
        if (user == null)
        {
            _logger.LogWarning("Password reset requested for non-existing email {Email}", LogSanitizer.SanitizeAndMask(command.Email));
            return Outcome.Success();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var callbackUrl = $"{command.RedirectUrl}?user_id={user.Id}&code={code}";

        await _emailSender.SendEmailAsync(
            user.Email!,
            "Reset your password",
            $"To reset your password click <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>here</a>.");

        _logger.LogInformation("Password reset email sent to {Email}", LogSanitizer.SanitizeAndMask(user.Email));
        return Outcome.Success();
    }
}
