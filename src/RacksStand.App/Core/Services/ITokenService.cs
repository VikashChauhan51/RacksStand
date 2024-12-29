namespace RacksStand.App.Core.Services;

public interface ITokenService
{
    Task<string?> GetAccessTokenAsync();
    Task SetAccessTokenAsync(string token);
    Task<bool> IsAccessTokenValidAsync();
    Task RefreshAccessTokenAsync();
}
