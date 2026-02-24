namespace RacksStand.Api.Endpoints.Auth.SignUp.Handlers;

using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using RacksStand.Api.Core.Logging;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.SignUp.Models;
using System.Text;
using System.Text.Encodings.Web;

public sealed class SignUpEmailHandler : ICommandHandler<SignUpEmailRequest>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<SignUpEmailHandler> _logger;

    public SignUpEmailHandler(
        ILogger<SignUpEmailHandler> logger,
        UserManager<ApplicationUser> userManager,
        IUserStore<ApplicationUser> userStore,
        IEmailSender emailSender)
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailSender = emailSender;
        _logger = logger;
    }
    public async Task ExecuteAsync(SignUpEmailRequest command, CancellationToken ct)
    {
        var user = await _userStore.FindByIdAsync(command.UserId, ct);
        if (user == null)
        {
            _logger.LogWarning("The user not found with id {UserId}", LogSanitizer.SanitizeAndMask(command.UserId));
            return;
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var callbackUrl = $"{command.RedirectUrl}?user_id={command.UserId}&code={code}";

        await _emailSender.SendEmailAsync(
            user.Email!,
            "Confirm your email",
            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
        );

        _logger.LogInformation("Confirmation email sent to {Email}", LogSanitizer.SanitizeAndMask(user.Email));
    }
}
