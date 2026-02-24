namespace RacksStand.Api.Core.Sms;

public interface ISmsSender
{
    Task SendSmsAsync(string phoneNumber, string message);
}
