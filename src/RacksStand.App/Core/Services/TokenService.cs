using Blazored.LocalStorage;

namespace RacksStand.App.Core.Services;

public class TokenService : ITokenService
{
    private readonly ILocalStorageService _localStorage;

    public TokenService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    // Get the Access Token from localStorage
    public async Task<string?> GetAccessTokenAsync()
    {
        return await _localStorage.GetItemAsync<string?>("access_token");
    }

    // Store the Access Token in localStorage
    public async Task SetAccessTokenAsync(string token)
    {
        await _localStorage.SetItemAsync("access_token", token);
    }

    // Check if the Access Token is valid
    public async Task<bool> IsAccessTokenValidAsync()
    {
        var token = await GetAccessTokenAsync();
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        // Optional: Implement token expiration check if the token has an expiration time
        // Example: if the token has an expiry date in its payload
        // You can decode the JWT token and check the expiration field, and validate if it's expired.

        return true;
    }

    // Refresh the Access Token
    public async Task RefreshAccessTokenAsync()
    {
        // Implement your refresh token logic here
        var refreshToken = await _localStorage.GetItemAsync<string>("refresh_token");

        if (!string.IsNullOrEmpty(refreshToken))
        {
            // Make a request to the backend to get a new access token using the refresh token
            var newAccessToken = await RefreshToken(refreshToken);

            // Store the new access token
            await SetAccessTokenAsync(newAccessToken);
        }
    }

    // A placeholder method to refresh the access token using the refresh token.
    // This should call your authentication API.
    private async Task<string> RefreshToken(string refreshToken)
    {
        // Implement the API call to get a new access token using the refresh token
        // For example, you might call your authentication server here:
        // var newToken = await _authenticationService.RefreshTokenAsync(refreshToken);
        // return newToken;

        return "newAccessToken"; // Replace with actual refresh logic
    }
}
