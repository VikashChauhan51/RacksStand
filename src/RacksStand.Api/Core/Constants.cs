namespace RacksStand.Api.Core;

public static class Constants
{
    public const string AuthenticationSectionName = "Authentication";
    public const string CachingSectionName = "Caching";
    public const int MaximumKeyLength = 1024;
    public const int MaxPayloadSizeBytes = 1024 * 1024; // 1 MB
    public const int MaxRetryCount = 3;
    public const int DefaultCacheExpirationMinutes = 5;
    public const int RetryDelayMilliseconds = 200;
}
