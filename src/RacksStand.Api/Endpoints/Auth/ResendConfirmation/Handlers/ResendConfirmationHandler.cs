namespace RacksStand.Api.Endpoints.Auth.ResendConfirmation.Handlers;

using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using FastEndpoints;
using RacksStand.Api.Core.Logging;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.ResendConfirmation.Models;
using ResultifyCore;

public sealed class ResendConfirmationHandler : ICommandHandler<ResendConfirmationRequest, Outcome>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<ResendConfirmationHandler> _logger;

    public ResendConfirmationHandler(UserManager<ApplicationUser> userManager, IEmailSender emailSender, ILogger<ResendConfirmationHandler> logger)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(ResendConfirmationRequest command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.Email))
        {
            return Outcome.Problem(new OutcomeError("InvalidRequest", "Email is required."));
        }

        var user = await _userManager.FindByEmailAsync(command.Email);
        // Do not reveal whether exists
        if (user == null)
        {
            _logger.LogWarning("Resend confirmation requested for non-existing email {Email}", LogSanitizer.SanitizeAndMask(command.Email));
            return Outcome.Success();
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = $"{command.RedirectUrl}?user_id={user.Id}&code={code}";
        await _emailSender.SendEmailAsync(user.Email!, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        _logger.LogInformation("Confirmation email resent to {Email}", LogSanitizer.SanitizeAndMask(user.Email));
        return Outcome.Success();
    }
}
