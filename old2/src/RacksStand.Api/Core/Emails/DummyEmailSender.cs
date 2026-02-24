namespace RacksStand.Api.Core.Emails;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
public class DummyEmailSender : IEmailSender
{
    private readonly ILogger<DummyEmailSender> _logger;

    public DummyEmailSender(ILogger<DummyEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Dummy implementation: log and complete.
        _logger.LogInformation("[DummyEmailSender] To: {Email}\n Subject: {Subject}\n Message: {Message}", email, subject, htmlMessage);
        return Task.CompletedTask;
    }
}
