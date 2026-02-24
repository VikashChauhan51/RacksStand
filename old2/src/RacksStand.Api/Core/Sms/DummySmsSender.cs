namespace RacksStand.Api.Core.Sms;

using Microsoft.Extensions.Logging;

public sealed class DummySmsSender : ISmsSender
{
    private readonly ILogger<DummySmsSender> _logger;
    public DummySmsSender(ILogger<DummySmsSender> logger)
    {
        _logger = logger;
    }

    public Task SendSmsAsync(string phoneNumber, string message)
    {
        _logger.LogInformation("[DummySmsSender] To: {Phone} Message: {Message}", phoneNumber, message);
        return Task.CompletedTask;
    }
}
