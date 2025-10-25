namespace RacksStand.Api.Endpoints.Auth.Mfa;

using FastEndpoints;
using RacksStand.Api.Endpoints.Auth.Mfa.Models;
using ResultifyCore;

public class SendPhoneCodeEndpoint : Endpoint<SendPhoneCodeRequest>
{
    private readonly ILogger<SendPhoneCodeEndpoint> _logger;
    public SendPhoneCodeEndpoint(ILogger<SendPhoneCodeEndpoint> logger) => _logger = logger;

    public override void Configure()
    {
        Post("/api/auth/mfa/send-sms");
        Version(1);
        AuthSchemes("Bearer");
        Summary(s => s.Summary = "Send phone verification code via SMS (uses ISmsSender).");
    }

    public override async Task HandleAsync(SendPhoneCodeRequest req, CancellationToken ct)
    {
        _logger.LogInformation("Sending phone verification code to {Phone}", req.PhoneNumber);
        var resp = await req.ExecuteAsync(ct);
        await Send.ResultAsync(resp.ToResult());
    }
}
